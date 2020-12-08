using System.IO;

namespace PbLab.DesignPatterns.Tools
{
    internal class FileProxy
    {
        public static StreamReader OpenText(string file)
        {
            return File.Exists(file) == false ? StreamReader.Null : File.OpenText(file);
        }
    }
}