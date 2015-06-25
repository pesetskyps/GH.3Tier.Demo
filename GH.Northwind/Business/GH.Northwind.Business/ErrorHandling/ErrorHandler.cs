using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using GH.Northwind.Business.Entities.Exceptions;
using log4net;

namespace GH.Northwind.Business.ErrorHandling
{
    public class ErrorHandler : IErrorHandler, IServiceBehavior
    {
        #region IErrorHandler Members
        private static readonly ILog log = LogManager.GetLogger(typeof(ErrorHandler));
        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error is FaultException) return;
            log.Error(error);
            BusinessServiceException businessFault = new BusinessServiceException(error);
            FaultException<BusinessServiceException> faultEx =
                new FaultException<BusinessServiceException>(businessFault, "Error occurs in business service",
                                                             new FaultCode("BusinessServiceException"));
            MessageFault msgFault = faultEx.CreateMessageFault();
            fault = Message.CreateMessage(version, msgFault, faultEx.Action);
        }

        #endregion

        #region IServiceBehavior Members

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
                                         Collection<ServiceEndpoint> endpoints,
                                         BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            IErrorHandler errHandler = new ErrorHandler();
            foreach (ChannelDispatcherBase dispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher dispatcher = dispatcherBase as ChannelDispatcher;
                if (dispatcher == null) continue;
                dispatcher.ErrorHandlers.Add(errHandler);
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        #endregion
    }
}