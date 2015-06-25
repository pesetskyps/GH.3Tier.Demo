using System;
using System.Runtime.Serialization;

namespace GH.Northwind.Business.Entities.Exceptions
{
    [DataContract(Namespace = "http://GH.Northwind.Business.Entities.Exceptions")]
    public class BusinessServiceException : Exception
    {
        public BusinessServiceException(Exception ex)
        {
            Source = ex.Source;
            Message = ex.Message;
            StackTrace = ex.StackTrace;
            HelpLink = ex.HelpLink;
        }

        [DataMember]
        public string StackTrace { get; set; }

        [DataMember]
        public string Source { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string HelpLink { get; set; }
    }
}