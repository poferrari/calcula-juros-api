using CalculaJuros.Dominio.Configuracoes;
using System;

namespace CalculaJuros.Dominio.CalculoJuros.Dtos
{
    public class EntradaCalculo
    {
        public decimal ValorInicial { get; set; }
        public int Meses { get; set; }

        public EntradaCalculo(decimal valorInicial, int meses)
        {
            ValorInicial = valorInicial;
            Meses = meses;
        }

        public void ValidarEntrada()
        {
            if (ValorInicial <= 0)
            {
                throw new InvalidOperationException(Constantes.MENSAGEM_ERRO_VALOR_INICIAL);
            }

            if (Meses <= 0)
            {
                throw new InvalidOperationException(Constantes.MENSAGEM_ERRO_MESES);
            }
        }

        public decimal CalcularJurosCompostos(decimal taxaDeJuros)
        {
            ValidarEntrada();

            var valorFinal = ValorInicial * CalcularTaxaDeJurosAplicada(taxaDeJuros);

            return TruncarValor(valorFinal);
        }

        public decimal CalcularTaxaDeJurosAplicada(decimal taxaDeJuros)
            => (decimal)Math.Pow((1 + (double)taxaDeJuros), Meses);

        public decimal TruncarValor(decimal valorFinal)
            => Math.Truncate(valorFinal * 100) / 100;
    }
}
