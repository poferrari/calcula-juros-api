using AutoFixture;
using CalculaJuros.Dominio.Configuracoes;
using CalculaJuros.Dominio.Configuracoes.Util;
using CalculaJuros.Dominio.TaxaJuros.Dtos;
using CalculaJuros.Dominio.TaxaJuros.Servicos;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using dto = CalculaJuros.Dominio.TaxaJuros.Dtos;

namespace CalculaJuros.Tests.TaxaJuros.Servicos
{
    [TestFixture]
    public class TaxaJurosServicoTests
    {
        private Fixture _fixture;
        private TaxaJurosServico _taxaJurosServico;
        private Mock<TaxaJurosConfig> _taxaDeJurosConfig;
        private Mock<HttpHelper> _httpHelper;
        private decimal _valorTaxaDeJuros;
        private string _baseUrlApiTaxaJuros;
        private dto.TaxaJuros _taxaJuros;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _valorTaxaDeJuros = _fixture.Create<decimal>();
            _baseUrlApiTaxaJuros = _fixture.Create<string>();
            _taxaJuros = _fixture.Build<dto.TaxaJuros>()
                                 .With(t => t.Valor, _valorTaxaDeJuros)
                                 .Create();

            _httpHelper = new Mock<HttpHelper>(MockBehavior.Strict);
            _taxaDeJurosConfig = new Mock<TaxaJurosConfig>(MockBehavior.Strict);
            _taxaDeJurosConfig.SetupGet(t => t.BaseUrl)
                .Returns(_baseUrlApiTaxaJuros);

            _taxaJurosServico = new TaxaJurosServico(_taxaDeJurosConfig.Object, _httpHelper.Object);
        }

        [Test]
        public async Task Retornar_taxa_de_juros_com_sucesso()
        {
            _httpHelper.Setup(s => s.Get<dto.TaxaJuros>(_baseUrlApiTaxaJuros))
                .ReturnsAsync(_taxaJuros);

            var valorTaxaJuros = await _taxaJurosServico.ObterTaxa();

            valorTaxaJuros.Should().Be(_valorTaxaDeJuros);
            _httpHelper.Verify();
        }

        [Test]
        public async Task Retornar_taxa_de_juros_com_falha_comunicacao_com_sucesso()
        {
            var primeiraTentativa = true;
            _httpHelper.Setup(s => s.Get<dto.TaxaJuros>(_baseUrlApiTaxaJuros))
                .Callback(() =>
                {
                    if (primeiraTentativa)
                    {
                        primeiraTentativa = false;
                        throw new InvalidOperationException();
                    }
                })
                .ReturnsAsync(_taxaJuros);

            var valorTaxaJuros = await _taxaJurosServico.ObterTaxa();

            valorTaxaJuros.Should().Be(_valorTaxaDeJuros);
            _httpHelper.Verify(v => v.Get<dto.TaxaJuros>(_baseUrlApiTaxaJuros), Times.Exactly(2));
        }

        [Test]
        public void Retornar_taxa_de_juros_com_tentativas_maxima()
        {
            var contadorTentativas = 1;
            _httpHelper.Setup(s => s.Get<dto.TaxaJuros>(_baseUrlApiTaxaJuros))
                .Callback(() =>
                {
                    if (contadorTentativas < Constantes.MAXIMO_TENTATIVAS)
                    {
                        contadorTentativas++;
                        throw new InvalidOperationException();
                    }
                });

            Func<Task> act = async () => await _taxaJurosServico.ObterTaxa();

            act.Should().Throw<Exception>();
            _httpHelper.Verify(v => v.Get<dto.TaxaJuros>(_baseUrlApiTaxaJuros), Times.Exactly(Constantes.MAXIMO_TENTATIVAS));
        }
    }
}
