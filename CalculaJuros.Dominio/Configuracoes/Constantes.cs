namespace CalculaJuros.Dominio.Configuracoes
{
    public static class Constantes
    {
        public const string NOME_API = "API para calcular juros";
        public const string DESCRICAO_API = "Retornar o resultado do cálculo de juros";
        public const string MENSAGEM_ERRO_REPOSITORIOS_GIT = "Problemas ao apresentar os repositórios do GitHub.";
        public const string MENSAGEM_ERRO_TAXA_INVALIDA = "Problemas ao obter a taxa de juros";
        public const string MENSAGEM_ERRO_CALCULO = "Não foi possível calcular, tente novamente mais tarde";
        public const string MENSAGEM_ERRO_VALOR_INICIAL = "Valor inicial inválido";
        public const string MENSAGEM_ERRO_MESES = "Quantidade de meses inválido";
        public const int MAXIMO_TENTATIVAS = 3;
        public const int PAUSA_ENTRE_FALHAS = 2;
        public const int MINUTOS_CACHE = 5;
        public const string CHAVE_CACHE_GIT = "repositorios.git";
    }
}
