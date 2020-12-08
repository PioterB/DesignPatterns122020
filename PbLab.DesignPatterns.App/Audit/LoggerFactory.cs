namespace PbLab.DesignPatterns.Audit
{
    public class LoggerFactory
    {
        private readonly ILogger _composedLogger = new ComposedLogger();

        public ILogger Create()
        {
            return _composedLogger;
        }
    }
}