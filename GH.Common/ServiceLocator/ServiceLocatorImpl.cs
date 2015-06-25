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
using Ninject;

namespace GH.Common.ServiceLocator
{
    public class ServiceLocatorImpl<IService> : IServiceLocator<IService>
    {
        /// <summary>
        ///   Get IService instance
        /// </summary>
        /// <returns></returns>
        public IService GetService()
        {
            return NinjectWraper.Kernel.Get<IService>();
        }

        /// <summary>
        /// </summary>
        /// <param name = "type"> an implementation type of this interface </param>
        /// <returns></returns>
        public IService GetServiceByType(Type type)
        {
            return (IService) NinjectWraper.Kernel.Get(type);
        }

        /// <summary>
        ///   Register IService
        /// </summary>
        /// <param name = "service">A physical instance of IService</param>
        public void RegisterService<T>() where T : class, IService
        {
            NinjectWraper.Kernel.Bind<IService>().To<T>();
        }


        /// <summary>
        ///   UnRegister IService
        /// </summary>
        /// <param name = "service">A physical instance of IService</param>
        public void UnRegisterService()
        {
            NinjectWraper.Kernel.Unbind<IService>();
        }
    }
}