using System.Collections;
using System.Collections.Generic;
using PbLab.DesignPatterns.Model;

namespace PbLab.DesignPatterns.Services
{
    public class MySamplesCollection : IEnumerable<Sample>
    {
        public IEnumerator<Sample> GetEnumerator()
        {
            return new MySamplesCollectionEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}