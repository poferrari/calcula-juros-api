namespace CalculaJuros.Dominio.Configuracoes.Dtos
{
    public class RespostaErro
    {
        public string Mensagem { get; set; }

        public RespostaErro(string mensagem)
            => Mensagem = mensagem;
    }
}
