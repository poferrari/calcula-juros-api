using CalculaJuros.Dominio.CalculoJuros.Dtos;
using CalculaJuros.Dominio.CalculoJuros.Servicos;
using CalculaJuros.Dominio.Configuracoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CalculaJuros.Api.Controllers.V1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/calculaJuros")]
    public class CalculaJurosController : ControllerPadrao<CalculaJurosController>
    {
        private readonly ICalculaJurosServico _calculaJurosServico;

        public CalculaJurosController(ILogger<CalculaJurosController> logger,
                                      ICalculaJurosServico calculaJurosServico)
            : base(logger)
        {
            _calculaJurosServico = calculaJurosServico;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] EntradaCalculo entrada)
        {
            try
            {
                var resultado = await _calculaJurosServico.Calcular(entrada);
                return CriadoSucesso(resultado);
            }
            catch (Exception ex)
            {
                return RetornoErro(ex, Constantes.MENSAGEM_ERRO_CALCULO);
            }
        }
    }
}
