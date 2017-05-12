using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public IDictionary GetResourceCache(string cultureName)
        {
            IDictionary cache = (IDictionary)_caches[cultureName];
            if (cache == null)
            {
                // TODO do sql lookup here
                cache = new HybridDictionary();
                cache[cultureName] = cache;
            }
            return cache;
        }
    }
}