using System;

namespace PbLab.DesignPatterns.Services
{
    public class ChanelFactory : IChanelFactory
    {
        public IChanel Create(string protocol)
        {
            IChanel result = new NullChanel();

            switch (protocol)
            {
                case "file": result = new FileChanel(); break;
                case "http": result = new HttpChanel(); break;
            }

            return result;
        }
    }
}