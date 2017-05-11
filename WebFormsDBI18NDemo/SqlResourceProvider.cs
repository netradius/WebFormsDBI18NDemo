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

namespace WebFormsDBI18NDemo
{
    public sealed class SqlResourceProvider : IResourceProvider
    {

        private string _virtualPath;
        private string _className;
        private IDictionary _resourceCache;
        private static object CultureNeutralKey = new object();

        public SqlResourceProvider(string virtualPath, string className)
        {
            _virtualPath = virtualPath;
            _className = className;
        }

        private IDictionary GetResourceCache(string cultureName)
        {
            object cultureKey;
            if (cultureName != null)
            {
                cultureKey = cultureName;
            }
            else
            {
                cultureKey = CultureNeutralKey;
            }

            if (_resourceCache == null)
            {
                _resourceCache = new ListDictionary();
            }
            IDictionary resourceDict = _resourceCache[cultureKey] as IDictionary;
            if (resourceDict == null)
            {
                resourceDict = SqlResourceHelper.GetResources(_virtualPath,
                              _className, cultureName, false, null);
                _resourceCache[cultureKey] = resourceDict;
            }
            return resourceDict;
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

            object value = GetResourceCache(cultureName)[resourceKey];
            //if (value == null)
            //{
            //    // resource is missing for current culture, use default
            //    SqlResourceHelper.AddResource(resourceKey,
            //            _virtualPath, _className, cultureName);
            //    value = GetResourceCache(null)[resourceKey];
            //}
            //if (value == null)
            //{
            //    // the resource is really missing, no default exists
            //    SqlResourceHelper.AddResource(resourceKey,
            //         _virtualPath, _className, string.Empty);
            //}
            return value;
        }
        IResourceReader IResourceProvider.ResourceReader
        {
            get
            {
                return new SqlResourceReader(GetResourceCache(null));
            }
        }
    }
}