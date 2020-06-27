using AutoFixture;
using CalculaJuros.Api.Controllers.V1;
using CalculaJuros.Dominio.Configuracoes;
using CalculaJuros.Dominio.Configuracoes.Dtos;
using CalculaJuros.Dominio.RepositoriosGit.Dtos;
using CalculaJuros.Dominio.RepositoriosGit.Servicos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Net;

namespace CalculaJuros.Tests.Controllers.V1
{
    [TestFixture]
    public class ShowMeTheCodeControllerTests
    {
        private Fixture _fixture;
        private Mock<ILogger<ShowMeTheCodeController>> _logger;
        private Mock<IRepositorioGitServico> _repositorioGitServico;
        private ShowMeTheCodeController _showMeTheCodeController;
        private RepositorioGit _repositorioGit;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _repositorioGit = _fixture.Create<RepositorioGit>();

            _logger = new Mock<ILogger<ShowMeTheCodeController>>();
            _repositorioGitServico = new Mock<IRepositorioGitServico>(MockBehavior.Strict);

            _showMeTheCodeController = new ShowMeTheCodeController(_logger.Object, _repositorioGitServico.Object);
        }

        [Test]
        public void Retornar_repositorios_com_sucesso()
        {
            _repositorioGitServico.Setup(s => s.ObterLinks())
                .Returns(_repositorioGit);

            var retorno = _showMeTheCodeController.Get();
            var resultadoOk = retorno as OkObjectResult;

            retorno.Should().NotBeNull();
            resultadoOk.StatusCode.Should().Be((int)HttpStatusCode.OK);
            resultadoOk.Value.As<RepositorioGit>().Should().Be(_repositorioGit);
            _repositorioGitServico.Verify();
        }

        [Test]
        public void Retornar_taxa_de_juros_com_problema()
        {
            var mensagemEsperada = Constantes.MENSAGEM_ERRO_REPOSITORIOS_GIT;
            var excecao = new InvalidOperationException("Erro");
            _repositorioGitServico.Setup(s => s.ObterLinks())
                .Throws(excecao);
            var respostaEsperada = new RespostaErro(mensagemEsperada);

            var retorno = _showMeTheCodeController.Get();
            var resultadoBadRequest = retorno as BadRequestObjectResult;

            retorno.Should().NotBeNull();
            resultadoBadRequest.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            resultadoBadRequest.Value.Should().BeEquivalentTo(respostaEsperada);
            _repositorioGitServico.Verify();
            _logger.Invocations[0].Arguments[0].As<LogLevel>().Should().Be(LogLevel.Error);
        }
    }
}
