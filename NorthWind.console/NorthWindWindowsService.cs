using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using GH.Northwind.Business;
using System.Configuration;
using System.Configuration.Install;
using System.IO;
using GH.Common.LogService;
using log4net;

namespace NorthWind.console
{
    class NorthWindWindowsService : ServiceBase
    {
        public ServiceHost serviceHost = null;
        private static readonly ILog log = LogManager.GetLogger(typeof(NorthWindWindowsService));
        public NorthWindWindowsService()
        {
            // Name the Windows Service
            ServiceName = "myservice";
        }

        public static void Main()
        {
            ServiceBase.Run(new NorthWindWindowsService());
        }

        // Start the Windows service.
        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            // Create a ServiceHost for the CalculatorService type and 
            // provide the base address.
            serviceHost = new ServiceHost(typeof(NorthwindSvr));

            // Open the ServiceHostBase to create listeners and start 
            // listening for messages.
            serviceHost.Open();
            File.Create("c:\\temp\\bbbbb.txt");
            log.Debug(string.Format("Service {0} is started", ServiceName));
        }
        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
                log.Debug(string.Format("Service {0} is stopped", ServiceName));
            }
        }

        [RunInstaller(true)]
        public class ProjectInstaller : Installer
        {
            private ServiceProcessInstaller process;
            private ServiceInstaller service;

            public ProjectInstaller()
            {
                process = new ServiceProcessInstaller();
                process.Account = ServiceAccount.LocalSystem;
                service = new ServiceInstaller();
                service.ServiceName = "WCFWindowsServiceSample";
                Installers.Add(process);
                Installers.Add(service);
            }
        }
    }
}
