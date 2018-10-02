using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Findie.ServerConfigurator.FindieLogger
{
    public static class Logger
    {
        public static void Log(string message, LoggerMessages loggerMessages)
        {
            switch (loggerMessages)
            {
                case LoggerMessages.Unknown:
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case LoggerMessages.Info:
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case LoggerMessages.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LoggerMessages.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    goto case LoggerMessages.Unknown;
            }
            
            Console.WriteLine(message);
        }

        public static void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            throw new NotImplementedException();
        }

        public static bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public static IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}