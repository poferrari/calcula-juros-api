using Flurl.Http;
using System.Threading.Tasks;

namespace CalculaJuros.Dominio.Configuracoes.Util
{
    public class HttpHelper
    {
        public virtual async Task<T> Get<T>(string baseUrl)
            => await baseUrl.GetJsonAsync<T>();
    }
}
