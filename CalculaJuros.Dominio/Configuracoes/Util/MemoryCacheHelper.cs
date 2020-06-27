using Microsoft.Extensions.Caching.Memory;
using System;

namespace CalculaJuros.Dominio.Configuracoes.Util
{
    public class MemoryCacheHelper
    {
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _opcoesCache;

        public MemoryCacheHelper(IMemoryCache cache)
        {
            _cache = cache;
            _opcoesCache = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(Constantes.MINUTOS_CACHE));
        }

        public T ObterValorCache<T>(T valor, string cacheKey)
        {
            try
            {
                var valorCache = _cache.Get<T>(cacheKey);
                return valorCache == null ?
                        AtribuirCacheNoValor(valor, cacheKey) :
                        valorCache;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private T AtribuirCacheNoValor<T>(T valor, string cacheKey)
        {
            _cache.Set(cacheKey, valor, _opcoesCache);
            return valor;
        }
    }
}
