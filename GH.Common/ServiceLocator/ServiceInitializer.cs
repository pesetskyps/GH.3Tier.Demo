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

namespace GH.Common.ServiceLocator
{
    public static class ServiceInitializer
    {
        private static IServiceInitializer _serviceInitializer;

        public static IServiceInitializer Initializer
        {
            get
            {
                if (_serviceInitializer == null) _serviceInitializer = new ServiceInitializerImp();
                return _serviceInitializer;
            }
            set { _serviceInitializer = value; }
        }

        public static void Init()
        {
            Initializer.Init();
        }
    }
}