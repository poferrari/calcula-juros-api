using CalculaJuros.Dominio.Configuracoes.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CalculaJuros.Api.Controllers
{
    [ApiController]
    public abstract class ControllerPadrao<T> : ControllerBase
    {
        protected readonly ILogger<T> _logger;

        protected ControllerPadrao(ILogger<T> logger)
        {
            _logger = logger;
        }

        protected OkObjectResult RetornoSucesso<E>(E resposta)
        {
            return Ok(resposta);
        }

        protected CreatedResult CriadoSucesso<E>(E resposta)
        {
            return Created(string.Empty, resposta);
        }

        protected BadRequestObjectResult RetornoErro(Exception ex, string mensagemErro)
        {
            _logger.LogError(ex, mensagemErro);

            var respota = new RespostaErro(mensagemErro);
            return BadRequest(respota);
        }
    }
}
