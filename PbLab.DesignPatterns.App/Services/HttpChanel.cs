using System;
using System.IO;
using System.Net;

namespace PbLab.DesignPatterns.Services
{
    public class HttpChanel : IChanel
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public StreamReader Connect(string resource)
        {
            var request = WebRequest.Create(resource);
            var response = request.GetResponse();
            var type = response.Headers["type"];
            return new StreamReader(response.GetResponseStream());
        }
    }
}