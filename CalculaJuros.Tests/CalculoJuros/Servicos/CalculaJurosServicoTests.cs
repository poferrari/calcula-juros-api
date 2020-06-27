using AutoFixture;
using CalculaJuros.Dominio.CalculoJuros.Dtos;
using CalculaJuros.Dominio.CalculoJuros.Servicos;
using CalculaJuros.Dominio.TaxaJuros.Servicos;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CalculaJuros.Tests.CalculoJuros.Servicos
{
    [TestFixture]
    public class CalculaJurosServicoTests
    {
        private Fixture _fixture;
        private Mock<ITaxaJurosServico> _taxaJurosServico;
        private CalculaJurosServico _calculaJurosServico;
        private EntradaCalculo _entrada;
        private readonly decimal _taxaDeJuros = 0.01m;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _taxaJurosServico = new Mock<ITaxaJurosServico>(MockBehavior.Strict);
            _entrada = _fixture.Build<EntradaCalculo>()
                               .With(t => t.Meses, 5)
                               .With(t => t.ValorInicial, 100)
                               .Create();

            _calculaJurosServico = new CalculaJurosServico(_taxaJurosServico.Object);
        }

        [Test]
        public async Task Calcular_jutos_composto_com_sucesso()
        {
            var valorFinalEsperado = 105.1m;
            var resultadoEsperado = new ResultadoCalculo(valorFinalEsperado);
            _taxaJurosServico.Setup(t => t.ObterTaxa())
                .ReturnsAsync(_taxaDeJuros);

            var valorFinal = await _calculaJurosServico.Calcular(_entrada);

            valorFinal.Should().BeEquivalentTo(resultadoEsperado);
        }
    }
}
