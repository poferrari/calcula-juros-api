using AutoFixture;
using CalculaJuros.Api.Controllers.V1;
using CalculaJuros.Dominio.CalculoJuros.Dtos;
using CalculaJuros.Dominio.CalculoJuros.Servicos;
using CalculaJuros.Dominio.Configuracoes;
using CalculaJuros.Dominio.Configuracoes.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CalculaJuros.Tests.Controllers.V1
{
    [TestFixture]
    public class CalculaJurosControllerTests
    {
        private Fixture _fixture;
        private Mock<ILogger<CalculaJurosController>> _logger;
        private Mock<ICalculaJurosServico> _calculaJurosServico;
        private CalculaJurosController _calculaJurosController;
        private EntradaCalculo _entrada;
        private decimal _valorFinalEsperado = 105.1m;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _entrada = _fixture.Build<EntradaCalculo>()
                           .With(t => t.Meses, 5)
                           .With(t => t.ValorInicial, 100)
                           .Create();

            _logger = new Mock<ILogger<CalculaJurosController>>();
            _calculaJurosServico = new Mock<ICalculaJurosServico>(MockBehavior.Strict);

            _calculaJurosController = new CalculaJurosController(_logger.Object, _calculaJurosServico.Object);
        }

        [Test]
        public async Task Calcular_jutos_composto_com_sucesso()
        {            
            var resultadoEsperado = new ResultadoCalculo(_valorFinalEsperado);
            _calculaJurosServico.Setup(s => s.Calcular(_entrada))
                .ReturnsAsync(resultadoEsperado);

            var retorno = await _calculaJurosController.Get(_entrada);
            var resultadoOk = retorno as OkObjectResult;

            retorno.Should().NotBeNull();
            resultadoOk.StatusCode.Should().Be((int)HttpStatusCode.OK);
            resultadoOk.Value.Should().BeEquivalentTo(resultadoEsperado);
            _calculaJurosServico.Verify();
        }

        [Test]
        public async Task Retornar_taxa_de_juros_com_problema()
        {
            var mensagemEsperada = Constantes.MENSAGEM_ERRO_CALCULO;
            var excecao = new InvalidOperationException("Erro");
            _calculaJurosServico.Setup(s => s.Calcular(_entrada))
                .ThrowsAsync(excecao);
            var respostaEsperada = new RespostaErro(mensagemEsperada);

            var retorno = await _calculaJurosController.Get(_entrada);
            var resultadoBadRequest = retorno as BadRequestObjectResult;

            retorno.Should().NotBeNull();
            resultadoBadRequest.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            resultadoBadRequest.Value.Should().BeEquivalentTo(respostaEsperada);
            _logger.Invocations[0].Arguments[0].As<LogLevel>().Should().Be(LogLevel.Error);
        }
    }
}
