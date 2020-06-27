using CalculaJuros.Dominio.Configuracoes;
using CalculaJuros.Dominio.RepositoriosGit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CalculaJuros.Api.Controllers.V1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/showmethecode")]
    public class ShowMeTheCodeController : ControllerPadrao<ShowMeTheCodeController>
    {
        private readonly RepositorioGit _repositoriosGit;

        public ShowMeTheCodeController(ILogger<ShowMeTheCodeController> logger,
                                       RepositorioGit repositoriosGit)
            : base(logger)
        {
            _repositoriosGit = repositoriosGit;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return RetornoSucesso(_repositoriosGit);
            }
            catch (Exception ex)
            {
                return RetornoErro(ex, Constantes.MENSAGEM_ERRO_REPOSITORIOS_GIT);
            }
        }
    }
}
