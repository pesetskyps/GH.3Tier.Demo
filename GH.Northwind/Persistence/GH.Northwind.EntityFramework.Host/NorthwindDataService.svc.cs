using System.Data.Objects;
using System.Data.Services;
using System.Data.Services.Common;
using System.ServiceModel;

namespace GH.Northwind.EntityFramework.Host
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class NorthwindDataService : DataService<ObjectContext>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // Allow full access rights on all entity sets
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;

            // return more information in the error response message for troubleshooting
            config.UseVerboseErrors = true;
        }

        protected override ObjectContext CreateDataSource()
        {
            var ctx = new NorthwindEntities();
            var oContext = ctx.ObjectContext;
            oContext.ContextOptions.ProxyCreationEnabled = false;
            return oContext;
        }
    }
}