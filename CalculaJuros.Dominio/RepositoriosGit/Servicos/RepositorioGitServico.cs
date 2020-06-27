using CalculaJuros.Dominio.Configuracoes;
using CalculaJuros.Dominio.Configuracoes.Util;
using dto = CalculaJuros.Dominio.RepositoriosGit.Dtos;

namespace CalculaJuros.Dominio.RepositoriosGit.Servicos
{
    public class RepositorioGitServico : IRepositorioGitServico
    {
        private readonly MemoryCacheHelper _memoryCacheHelper;
        private readonly dto.RepositorioGit _repositoriosGit;

        public RepositorioGitServico(MemoryCacheHelper memoryCacheHelper,
                                     dto.RepositorioGit repositoriosGit)
        {
            _memoryCacheHelper = memoryCacheHelper;
            _repositoriosGit = repositoriosGit;
        }

        public dto.RepositorioGit ObterLinks()
        {
            return _memoryCacheHelper.ObterValorCache(_repositoriosGit, Constantes.CHAVE_CACHE_GIT);
        }
    }
}
