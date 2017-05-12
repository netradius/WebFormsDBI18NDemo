using System.Collections;
using System.Collections.Specialized;

namespace WebFormsDBI18NDemo
{
    public class ResourceCacheManager
    {
        // Remember this isn't really a true singlement in ASP.NET because you can have multiple worker processes
        private static ResourceCacheManager _instance;

        private IDictionary _caches = new HybridDictionary();

        private ResourceCacheManager() { }

        public static ResourceCacheManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ResourceCacheManager();
                }
                return _instance;
            }
        }

        public IDictionary GetResourceCache(string virtualPath, string className, string cultureName)
        {
            IDictionary cache = (IDictionary)_caches[cultureName];
            if (cache == null)
            {
                cache = SqlResourceHelper.GetResources(virtualPath, className, cultureName, false, null);
                cache[cultureName] = cache;
            }
            return cache;
        }
    }
}