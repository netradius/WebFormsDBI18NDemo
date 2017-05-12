using System.Web.Compilation;
using log4net;

namespace WebFormsDBI18NDemo
{
    public sealed class SqlResourceProviderFactory : ResourceProviderFactory
    {

        static ILog log = LogManager.GetLogger(typeof(SqlResourceProviderFactory));

        public SqlResourceProviderFactory()
        {
        }

        public override IResourceProvider CreateGlobalResourceProvider(string classKey)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug("Creating global resource provider for class key: " + classKey);
            }
            return new SqlResourceProvider(null, classKey);
        }

        public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug("Creating local resource provier for virtual path: " + virtualPath);
            }
            virtualPath = System.IO.Path.GetFileName(virtualPath);
            return new SqlResourceProvider(virtualPath, null);
        }
    }
}