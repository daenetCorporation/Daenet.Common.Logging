using Microsoft.Extensions.Logging;

//TODO: Check Signature
//TODO: Refactor all
//TODO: Property for tracing in UTC

namespace Daenet.Common.Logging
{
    public class LogManager<T> : LogManager
    {
        public LogManager(ILoggerFactory loggerFactory,T type) : base(loggerFactory, type.GetType().FullName)
        {

        }
    }
}

