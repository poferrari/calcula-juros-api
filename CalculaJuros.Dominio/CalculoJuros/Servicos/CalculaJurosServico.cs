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

        public async Task<decimal> Calcular(EntradaCalculo entrada)
        {
            var taxaDeJuros = await _taxaJurosServico.ObterTaxa();

            return entrada.CalcularJurosCompostos(taxaDeJuros);
        }
    }
}
