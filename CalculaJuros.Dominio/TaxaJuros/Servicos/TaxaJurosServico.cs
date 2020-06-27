using CalculaJuros.Dominio.Configuracoes;
using CalculaJuros.Dominio.Configuracoes.Util;
using CalculaJuros.Dominio.TaxaJuros.Dtos;
using Polly;
using Polly.Retry;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using dto = CalculaJuros.Dominio.TaxaJuros.Dtos;

namespace CalculaJuros.Dominio.TaxaJuros.Servicos
{
    public class TaxaJurosServico : ITaxaJurosServico
    {
        private readonly TaxaJurosConfig _taxaDeJurosConfig;
        private readonly HttpHelper _httpHelper;
        private readonly AsyncRetryPolicy _retryPolicy;

        public TaxaJurosServico(TaxaJurosConfig taxaDeJurosConfig, HttpHelper httpHelper)
        {
            _taxaDeJurosConfig = taxaDeJurosConfig;
            _httpHelper = httpHelper;
            _retryPolicy = Policy.Handle<HttpRequestException>()
                                 .Or<InvalidOperationException>()
                                 .WaitAndRetryAsync(Constantes.MAXIMO_TENTATIVAS,
                                    tentativa => TimeSpan.FromSeconds(Math.Pow(Constantes.PAUSA_ENTRE_FALHAS, tentativa)));
        }

        public async Task<decimal> ObterTaxa()
        {
            decimal valorTaxaJuros = 0;

            await _retryPolicy.ExecuteAsync(async () =>
            {
                var taxaJuros = await _httpHelper.Get<dto.TaxaJuros>(_taxaDeJurosConfig.BaseUrl);

                if (taxaJuros is null)
                {
                    throw new InvalidOperationException(Constantes.MENSAGEM_ERRO_TAXA_INVALIDA);
                }

                valorTaxaJuros = taxaJuros.Valor;
            });

            return valorTaxaJuros;
        }
    }
}
