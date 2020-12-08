using System;
using System.Collections.Generic;
using System.Linq;

namespace PbLab.DesignPatterns.Services
{
    public class LocalFileReaderPool
    {
        private readonly ISamplesReaderFactory _readerFactory;

        private int _limit = 2;
        readonly IDictionary<string, Stack<ISamplesReader>> _free = new Dictionary<string, Stack<ISamplesReader>>();
        readonly ISet<Borrowed> _borrowed = new HashSet<Borrowed>();

        public LocalFileReaderPool(ISamplesReaderFactory readerFactory)
        {
            _readerFactory = readerFactory;
        }

        public void Release(ISamplesReader used)
        {
            var known = _borrowed.FirstOrDefault(b => b.Reader == used);

            if (known == null)
            {
                return;
            }

            _borrowed.Remove(known);
            _free[known.Key].Push(known.Reader);
        }

        public ISamplesReader Borrow(string key)
        {
            if (_free.ContainsKey(key) == false)
            {
                Initialize(key);
            }

            if (_free[key].Count == 0)
            {
                throw new Exception("pool is empty");
            }

            var returned = _free[key].Pop();

            _borrowed.Add(new Borrowed(key, returned));

            return returned;
        }

        private ISamplesReader Factorize(string key)
        {
            return _readerFactory.Get(key);
        }

        private void Initialize(string key)
        {
            _free.Add(key, new Stack<ISamplesReader>(new[] {Factorize(key), Factorize(key)}));
        }

        private class Borrowed
        {
            public Borrowed(string key, ISamplesReader reader)
            {
                Key = key;
                Reader = reader;
            }
            public string Key { get;  }

            public ISamplesReader Reader { get; }
        }
    }
}