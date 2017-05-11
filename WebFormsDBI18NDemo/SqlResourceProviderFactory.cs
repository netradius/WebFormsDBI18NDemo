using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;



namespace WebFormsDBI18NDemo
{
    public sealed class SqlResourceProviderFactory : ResourceProviderFactory
    {
        public SqlResourceProviderFactory()
        {
        }

        public override IResourceProvider CreateGlobalResourceProvider(string classKey)
        {
            return new SqlResourceProvider(null, classKey);
        }
        public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
        {
            virtualPath = System.IO.Path.GetFileName(virtualPath);
            return new SqlResourceProvider(virtualPath, null);
        }
    }
}