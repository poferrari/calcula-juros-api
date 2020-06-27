namespace CalculaJuros.Dominio.CalculoJuros.Dtos
{
    public class ResultadoCalculo
    {
        public decimal ValorFinal { get; set; }

        public ResultadoCalculo(decimal valorFinal)
        {
            ValorFinal = valorFinal;
        }
    }
}
