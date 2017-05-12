using System;
using System.Resources;
using System.Collections;

namespace WebFormsDBI18NDemo
{
    public class SqlResourceReader : IResourceReader
    {
        private IDictionary _resources;
        public SqlResourceReader(IDictionary resources)
        {
            _resources = resources;
        }

        IDictionaryEnumerator IResourceReader.GetEnumerator()
        {
            return _resources.GetEnumerator();
        }

        void IResourceReader.Close()
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _resources.GetEnumerator();
        }

        void IDisposable.Dispose()
        {
        }
    }
}