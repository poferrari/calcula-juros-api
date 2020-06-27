using System.Threading.Tasks;

namespace CalculaJuros.Dominio.TaxaJuros.Servicos
{
    public interface ITaxaJurosServico
    {
        Task<decimal> ObterTaxa();
    }
}
