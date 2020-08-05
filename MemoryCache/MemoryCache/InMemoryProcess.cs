using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCacheCSharp
{
    public class InMemoryProcess
    {
        private MemoryCache _cache;
        public InMemoryProcess(MemoryCache cache)
        {
            _cache = cache;
        }
        public bool SetCache(CacheItem item,CacheItemPolicy policy)
        {
            return _cache.Add(item, policy);
        }
        public CacheItem GetCache(string nameKey)
        {
           return _cache.GetCacheItem(nameKey);
        }
        public bool CheckExist(string nameKey)
        {
            return _cache.Contains(nameKey);
        }
        public object RemoveCache(string nameKey)
        {
           return _cache.Remove(nameKey);
        }
        public IDictionary<string,object> GetMutipleCache(string[] keys)
        {
           return _cache.GetValues(keys);
        }
        public object AddOrGetExistingCache(CacheItem item,CacheItemPolicy policy)
        {
            return _cache.AddOrGetExisting(item, policy);
        }
    }
}
