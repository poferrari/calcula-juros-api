using System;

namespace CalculaJuros.Dominio.CalculoJuros.Dtos
{
    public class EntradaCalculo
    {
        public decimal ValorInicial { get; set; }
        public int Meses { get; set; }

        public void ValidarEntrada()
        {
            if (ValorInicial <= 0)
            {
                throw new InvalidOperationException();
            }

            if (Meses <= 0)
            {
                throw new InvalidOperationException();
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
