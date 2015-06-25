/*
 * Copyright © 2012
 * This code is for the codeproject article "A N-Tier Architecture Sample with ASP.NET MVC3, WCF and Entity Framework" at  
 * http://www.codeproject.com/Articles/434282/A-N-Tier-Architecture-Sample-with-ASP.NET-MVC3-WCF-and-Entity-Framework. 
 * Permission to use, copy or modify this software freely is hereby granted, 
 * provided that this copyright notice appears in the orginal or modified copies. 
 * 
 * This code isn't guaranteed to work correctly; it is your own responsibility for 
 * any result from using this code. 
 *                           
 * @description
 * 
 * @author  
 * @version July 18, 2012
 * @see
 * @since
 */

using System;

namespace GH.Common.LogService
{
    public static class Log<T>
    {
        private static ILogger<T> _logger;

        public static ILogger<T> LogProvider
        {
            get
            {
                if (_logger == null) _logger = new LoggerImplDefault<T>();
                return _logger;
            }
            set { _logger = value; }
        }

        public static LogLevel LogLevel
        {
            get { return LogProvider.LogLevel; }
        }

        public static void Debug(String msg)
        {
            LogProvider.Debug(msg);
        }

        public static void Info(String msg)
        {
            LogProvider.Info(msg);
        }

        public static void Warn(String msg)
        {
            LogProvider.Warn(msg);
        }

        public static void Warn(String msg, Exception ex)
        {
            LogProvider.Warn(msg, ex);
        }

        public static void Warn(Exception ex)
        {
            LogProvider.Warn(ex);
        }

        public static void Error(String msg)
        {
            LogProvider.Error(msg);
        }

        public static void Error(String msg, Exception ex)
        {
            LogProvider.Error(msg, ex);
        }

        public static void Error(Exception ex)
        {
            LogProvider.Error(ex);
        }

        public static void Fatal(String msg)
        {
            LogProvider.Fatal(msg);
        }

        public static void Fatal(String msg, Exception ex)
        {
            LogProvider.Fatal(msg, ex);
        }

        public static void Fatal(Exception ex)
        {
            LogProvider.Fatal(ex);
        }
    }
}