using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging.Console;

namespace Product_Management_Client.Logging
{

    public static class LoggerFactoryInstance
    {

        public static Microsoft.Extensions.Logging.ILoggerFactory LoggerFactory { get; }

        static LoggerFactoryInstance()
        {
            LoggerFactory = new Microsoft.Extensions.Logging.LoggerFactory();
            LoggerFactory.AddProvider(new ConsoleLoggerProvider(new ConsoleLoggerSettings()));

        }
    }
}
