using System;
using System.Web.Compilation;
using System.Globalization;
using System.Resources;
using log4net;

namespace WebFormsDBI18NDemo
{
    public sealed class SqlResourceProvider : IResourceProvider
    {
        static ILog log = LogManager.GetLogger(typeof(SqlResourceProvider));
        private string virtualPath;
        private string className;
        //private IDictionary _resourceCache;
        private static object CultureNeutralKey = new object();

        public SqlResourceProvider(string virtualPath, string className)
        {
            this.virtualPath = virtualPath;
            this.className = className;
        }

        object IResourceProvider.GetObject(string resourceKey, CultureInfo culture)
        {
            string cultureName = null;
            if (culture != null)
            {
                cultureName = culture.Name;
            }
            else
            {
                cultureName = CultureInfo.CurrentUICulture.Name;
            }

            if (log.IsDebugEnabled)
            {
                log.Debug("Getting resource provider for resource key " + resourceKey + " and culture " + cultureName);
            }

            object value = ResourceCacheManager.Instance.GetResourceCache(this.virtualPath, this.className, cultureName)[resourceKey];
            // if the value is not found and the culture is language-country, then try just language
            if (value == null)
            {
                string[] cultureInfo = cultureName.Split('-');
                value = ResourceCacheManager.Instance.GetResourceCache(this.virtualPath, this.className, cultureInfo[0])[resourceKey];
            }
            // if the value is not found, try no culture
            if (value == null)
            {
                value = ResourceCacheManager.Instance.GetResourceCache(this.virtualPath, this.className, null)[resourceKey];
            }
            return value;
        }

        IResourceReader IResourceProvider.ResourceReader
        {
            get
            {
                string cultureName = null;
                CultureInfo currentUICulture = CultureInfo.CurrentUICulture;
                if (!String.Equals(currentUICulture.Name, CultureInfo.InstalledUICulture.Name))
                {
                    cultureName = currentUICulture.Name;
                }
                return new SqlResourceReader(ResourceCacheManager.Instance.GetResourceCache(this.virtualPath, this.className, cultureName));
            }
        }
    }
}