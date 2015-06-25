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

using Ninject;

namespace GH.Common.ServiceLocator
{
    internal class NinjectWraper
    {
        private static IKernel _ninjectKernel;

        public static IKernel Kernel
        {
            get
            {
                if (_ninjectKernel == null)
                {
                    _ninjectKernel = new StandardKernel();
                }
                return _ninjectKernel;
            }
        }
    }
}