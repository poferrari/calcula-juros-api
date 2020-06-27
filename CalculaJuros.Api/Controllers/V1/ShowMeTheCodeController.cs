using CalculaJuros.Dominio.Configuracoes;
using CalculaJuros.Dominio.RepositoriosGit.Servicos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CalculaJuros.Api.Controllers.V1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/showmethecode")]
    public class ShowMeTheCodeController : ControllerPadrao<ShowMeTheCodeController>
    {
        private readonly IRepositorioGitServico _repositorioGitServico;

        public ShowMeTheCodeController(ILogger<ShowMeTheCodeController> logger,
                                       IRepositorioGitServico repositorioGitServico)
            : base(logger)
        {
            _repositorioGitServico = repositorioGitServico;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var resposta = _repositorioGitServico.ObterLinks();
                return RetornoSucesso(resposta);
            }
            catch (Exception ex)
            {
                return RetornoErro(ex, Constantes.MENSAGEM_ERRO_REPOSITORIOS_GIT);
            }
        }
    }
}
