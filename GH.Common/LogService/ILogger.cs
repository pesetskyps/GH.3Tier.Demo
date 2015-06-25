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
    public enum LogLevel
    {
        ALL = 1,
        DEBUG = 2,
        INFO = 3,
        WARN = 4,
        ERROR = 5,
        FATAL = 6,
        OFF = 500,
        UNKNOWN = 1000
    };

    public interface ILogger<T>
    {
        LogLevel LogLevel { get; }

        void Debug(String msg);
        void Info(String msg);

        void Warn(String msg);
        void Warn(Exception ex);
        void Warn(String msg, Exception ex);

        void Error(String msg);
        void Error(Exception ex);
        void Error(String msg, Exception ex);

        void Fatal(String msg);
        void Fatal(String msg, Exception ex);
        void Fatal(Exception ex);
    }
}