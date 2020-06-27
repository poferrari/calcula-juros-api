using CalculaJuros.Dominio.CalculoJuros.Dtos;
using CalculaJuros.Dominio.TaxaJuros.Servicos;
using System.Threading.Tasks;

namespace CalculaJuros.Dominio.CalculoJuros.Servicos
{
    public class CalculaJurosServico : ICalculaJurosServico
    {
        private readonly ITaxaJurosServico _taxaJurosServico;

        public CalculaJurosServico(ITaxaJurosServico taxaJurosServico)
        {
            _taxaJurosServico = taxaJurosServico;
        }

        public async Task<ResultadoCalculo> Calcular(EntradaCalculo entrada)
        {
            var taxaDeJuros = await _taxaJurosServico.ObterTaxa();
            var valorFinal = entrada.CalcularJurosCompostos(taxaDeJuros);

            return new ResultadoCalculo(valorFinal);
        }
    }
}
