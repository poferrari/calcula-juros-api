using CalculaJuros.Dominio.CalculoJuros.Dtos;
using System.Threading.Tasks;

namespace CalculaJuros.Dominio.CalculoJuros.Servicos
{
    public interface ICalculaJurosServico
    {
        Task<ResultadoCalculo> Calcular(EntradaCalculo entrada);
    }
}
