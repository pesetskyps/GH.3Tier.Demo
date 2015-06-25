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

namespace GH.Common.ServiceLocator
{
    public static class ServiceLocator<IService>
    {
        private static IServiceLocator<IService> _serviceLocator;

        public static IServiceLocator<IService> Locator
        {
            get
            {
                if (_serviceLocator == null) _serviceLocator = new ServiceLocatorImpl<IService>();
                return _serviceLocator;
            }
            set { _serviceLocator = value; }
        }


        /// <summary>
        ///   Get IService instance
        /// </summary>
        /// <returns></returns>
        public static IService GetService()
        {
            return Locator.GetService();
        }


        /// <summary>
        /// </summary>
        /// <param name = "type"> an implementation type of this interface </param>
        /// <returns></returns>
        public static IService GetServiceByType(Type type)
        {
            return Locator.GetServiceByType(type);
        }


        /// <summary>
        ///   Register IService
        /// </summary>
        /// <param name = "service">A physical instance of IService</param>
        public static void RegisterService<T>() where T : class, IService
        {
            Locator.RegisterService<T>();
        }


        /// <summary>
        ///   UnRegister IService
        /// </summary>
        /// <param name = "service">A physical instance of IService</param>
        public static void UnRegisterService()
        {
            Locator.UnRegisterService();
        }
    }
}