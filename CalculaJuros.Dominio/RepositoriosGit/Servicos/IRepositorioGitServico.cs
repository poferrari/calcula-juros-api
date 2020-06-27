using dto = CalculaJuros.Dominio.RepositoriosGit.Dtos;

namespace CalculaJuros.Dominio.RepositoriosGit.Servicos
{
    public interface IRepositorioGitServico
    {
        dto.RepositorioGit ObterLinks();
    }
}
