namespace PbLab.DesignPatterns.Tools
{
    public class IdGenerator
    {
        private int _last;
        private static IdGenerator _instance;

        public static IdGenerator Instance => _instance ?? (_instance =  new IdGenerator());

        public int Next()
        {
            return _last++;
        }
    }
}