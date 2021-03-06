﻿using CalculaJuros.Dominio.CalculoJuros.Servicos;
using CalculaJuros.Dominio.Configuracoes.Util;
using CalculaJuros.Dominio.RepositoriosGit.Servicos;
using CalculaJuros.Dominio.TaxaJuros.Dtos;
using CalculaJuros.Dominio.TaxaJuros.Servicos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using dto = CalculaJuros.Dominio.RepositoriosGit.Dtos;

namespace CalculaJuros.Infra.Modulos
{
    public static class DominioConfig
    {
        public static void Registrar(IServiceCollection services, IConfiguration configuration)
        {
            var taxaJurosConfig = configuration.GetSection("Parametros:TaxaJurosConfig").Get<TaxaJurosConfig>();
            services.AddSingleton(taxaJurosConfig);

            var repositorio = configuration.GetSection("Parametros:RepositorioGit").Get<dto.RepositorioGit>();
            services.AddSingleton(repositorio);

            services.AddSingleton<HttpHelper>();
            services.AddSingleton<MemoryCacheHelper>();

            services.AddScoped<IRepositorioGitServico, RepositorioGitServico>();
            services.AddScoped<ITaxaJurosServico, TaxaJurosServico>();
            services.AddScoped<ICalculaJurosServico, CalculaJurosServico>();
        }
    }
}
