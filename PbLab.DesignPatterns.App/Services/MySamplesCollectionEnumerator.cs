using System;
using System.Collections;
using System.Collections.Generic;
using PbLab.DesignPatterns.Model;

namespace PbLab.DesignPatterns.Services
{
    public class MySamplesCollectionEnumerator : IEnumerator<Sample>
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public Sample Current { get; }

        object IEnumerator.Current => Current;
    }
}