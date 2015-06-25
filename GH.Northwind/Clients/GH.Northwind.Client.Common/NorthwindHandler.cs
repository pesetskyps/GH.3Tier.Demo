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
using System.Configuration;
using System.ServiceModel;
using System.Runtime.Serialization; 
using GH.Northwind.Business;
using GH.Northwind.Business.Interfaces;

namespace GH.Northwind.Client.Common
{
    public class NorthwindHandler
    {
        public static INorthwindSvr GetNorthwindService()
        {
            if (ConfigurationSettings.AppSettings["N-Tier"].ToLower() == "true")
            {
                string northwindBusinessUrl = ConfigurationSettings.AppSettings["NorthwindSvrBusinessUrl"];
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.MaxReceivedMessageSize = 1000000;
                binding.ReaderQuotas.MaxDepth = 200; 
                ChannelFactory<INorthwindSvr> channelFactory = new ChannelFactory<INorthwindSvr>(
                    binding,
                    new EndpointAddress(new Uri(northwindBusinessUrl)));
                INorthwindSvr northwindSvr = channelFactory.CreateChannel();
                return northwindSvr;
            }
            else
            {
                return new NorthwindSvr();
            }
        }
    }
}