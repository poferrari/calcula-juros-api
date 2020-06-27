using CalculaJuros.Infra.Modulos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CalculaJuros.Infra
{
    public static class Dependencias
    {
        public static IServiceCollection ConfigurarContainer(this IServiceCollection services, IConfiguration configuration)
        {
            DominioConfig.Registrar(services, configuration);
            return services;
        }
    }
}
