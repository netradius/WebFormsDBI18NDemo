using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Globalization;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Resources;
using log4net;

namespace WebFormsDBI18NDemo
{
    public sealed class SqlResourceProvider : IResourceProvider
    {
        static ILog log = LogManager.GetLogger(typeof(SqlResourceProvider));
        private string _virtualPath;
        private string _className;
        //private IDictionary _resourceCache;
        private static object CultureNeutralKey = new object();

        public SqlResourceProvider(string virtualPath, string className)
        {
            _virtualPath = virtualPath;
            _className = className;
        }

        //private IDictionary GetResourceCache(string cultureName)
        //{
        //    if (log.IsDebugEnabled)
        //    {
        //        log.Debug("Getting resource cache for culture name: " + cultureName);
        //    }
        //    object cultureKey;
        //    if (cultureName != null)
        //    {
        //        cultureKey = cultureName;
        //    }
        //    else
        //    {
        //        cultureKey = CultureNeutralKey;
        //    }

        //    //if (_resourceCache == null)
        //    //{
        //        _resourceCache = new ListDictionary();
        //    //}
        //    IDictionary resourceDict = _resourceCache[cultureKey] as IDictionary;
        //    if (resourceDict == null)
        //    {
        //        resourceDict = SqlResourceHelper.GetResources(_virtualPath,
        //                      _className, cultureName, false, null);
        //        _resourceCache[cultureKey] = resourceDict;
        //    }
        //    return resourceDict;
        //}

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

            object value = ResourceCacheManager.Instance.GetResourceCache(_virtualPath, _className, cultureName)[resourceKey];
            // if the value is not found and the culture is language-country, then try just language
            if (value == null)
            {
                string[] cultureInfo = cultureName.Split('-');
                value = ResourceCacheManager.Instance.GetResourceCache(_virtualPath, _className, cultureInfo[0])[resourceKey];
            }
            // if the value is not found, try no culture
            if (value == null)
            {
                value = ResourceCacheManager.Instance.GetResourceCache(_virtualPath, _className, null)[resourceKey];
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
                return new SqlResourceReader(ResourceCacheManager.Instance.GetResourceCache(_virtualPath, _className, cultureName));
            }
        }
    }
}