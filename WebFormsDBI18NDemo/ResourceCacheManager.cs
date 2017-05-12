using log4net;
using System.Collections;
using System.Collections.Specialized;

namespace WebFormsDBI18NDemo
{
    public class ResourceCacheManager
    {
        // Remember this isn't really a true singlement in ASP.NET because you can have multiple worker processes
        private static ResourceCacheManager instance;

        static ILog log = LogManager.GetLogger(typeof(ResourceCacheManager));
        private IDictionary caches = new HybridDictionary();

        private ResourceCacheManager() { }

        public static ResourceCacheManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ResourceCacheManager();
                }
                return instance;
            }
        }

        public IDictionary GetResourceCache(string virtualPath, string className, string cultureName)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug("Looking up cache for virtual path: " + virtualPath + " class name: " + className + " culture name: " + cultureName);
            }
            string idx = cultureName == null ? "noculture" : cultureName;
            IDictionary cache = (IDictionary)this.caches[idx];
            if (cache == null)
            {
                cache = SqlResourceHelper.GetResources(virtualPath, className, cultureName, false, null);
                cache[idx] = cache;
            }
            return cache;
        }
    }
}