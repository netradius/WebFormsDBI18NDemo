using System;
using System.Resources;
using System.Collections;

namespace WebFormsDBI18NDemo
{
    public class SqlResourceReader : IResourceReader
    {
        private IDictionary resources;
        public SqlResourceReader(IDictionary resources)
        {
            this.resources = resources;
        }

        IDictionaryEnumerator IResourceReader.GetEnumerator()
        {
            return this.resources.GetEnumerator();
        }

        void IResourceReader.Close()
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.resources.GetEnumerator();
        }

        void IDisposable.Dispose()
        {
        }
    }
}