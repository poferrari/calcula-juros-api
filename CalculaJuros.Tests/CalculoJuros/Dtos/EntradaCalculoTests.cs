using AutoFixture;
using CalculaJuros.Dominio.CalculoJuros.Dtos;
using CalculaJuros.Dominio.Configuracoes;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace CalculaJuros.Tests.CalculoJuros.Dtos
{
    [TestFixture]
    public class EntradaCalculoTests
    {
        private Fixture _fixture;
        private EntradaCalculo _entradaCalculo;
        private readonly decimal _taxaDeJuros = 0.01m;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _entradaCalculo = _fixture.Create<EntradaCalculo>();
        }

        [Test]
        public void Validar_entrada_com_sucesso()
        {
            Action act = () => _entradaCalculo.ValidarEntrada();

            act.Should().NotThrow();
        }

        [Test]
        public void Validar_entrada_com_meses_invalido()
        {
            _entradaCalculo.Meses = -1;

            Action act = () => _entradaCalculo.ValidarEntrada();

            act.Should().Throw<InvalidOperationException>().WithMessage(Constantes.MENSAGEM_ERRO_MESES);
        }

        [Test]
        public void Validar_entrada_com_valor_inicial_invalido()
        {
            _entradaCalculo.ValorInicial = -1;

            Action act = () => _entradaCalculo.ValidarEntrada();

            act.Should().Throw<InvalidOperationException>().WithMessage(Constantes.MENSAGEM_ERRO_VALOR_INICIAL);
        }

        [TestCase(0, 0)]
        [TestCase(2.919, 2.91)]
        [TestCase(2.91111, 2.91)]
        [TestCase(2.1345, 2.13)]
        public void Truncar_valor_com_sucesso(decimal valor, decimal valorEsperado)
        {
            var valorTruncado = _entradaCalculo.TruncarValor(valor);

            valorTruncado.Should().Be(valorEsperado);
        }

        [TestCase(5, 1.0510100501)]
        [TestCase(12, 1.12682503013197)]
        [TestCase(24, 1.26973464853191)]
        public void Calcular_taxa_de_juros_aplicada(int meses, decimal valorEsperado)
        {
            _entradaCalculo.Meses = meses;

            var taxaAplicada = _entradaCalculo.CalcularTaxaDeJurosAplicada(_taxaDeJuros);

            taxaAplicada.Should().Be(valorEsperado);
        }

        [TestCase(100, 5, 105.1)]
        [TestCase(100, 12, 112.68)]
        [TestCase(120, 9, 131.24)]
        public void Calcular_juros_compostos_com_sucesso(decimal valorInicial, int meses, decimal valorEsperado)
        {
            _entradaCalculo = new EntradaCalculo(valorInicial, meses);

            var valorFinal = _entradaCalculo.CalcularJurosCompostos(_taxaDeJuros);

            valorFinal.Should().Be(valorEsperado);
        }
    }
}
