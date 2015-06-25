using System;
using System.ServiceModel.Configuration;

namespace GH.Northwind.Business.ErrorHandling
{
    public class ErrorHandlerBehavior : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof (ErrorHandler); }
        }

        protected override object CreateBehavior()
        {
            return new ErrorHandler();
        }
    }
}