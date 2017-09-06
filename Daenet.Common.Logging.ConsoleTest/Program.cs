using Daenet.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace Daenet.Common.Logging.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ILoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole(true);

            LogManager logManager = new LogManager(loggerFactory, "Daenet.Common.Logging.ConsoleTest");

            logManager.TraceError(TracingLevel.Level3, 3, "Hello World-1");

            logManager.AddScope("MachineName", Environment.MachineName); 
            logManager.TraceError(TracingLevel.Level3, 3, "Hello World-2");

            logManager.AddScope("Property1", "Value1");
            logManager.TraceError(TracingLevel.Level3, 3, "Hello World-3");

            Console.ReadLine();
        }
    }
}
