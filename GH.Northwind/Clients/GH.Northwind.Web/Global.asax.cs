using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GH.Common.LogService;
using GH.Northwind.Client.Common;
using GH.Northwind.Web.Controllers;
using GH.Northwind.Web.ModelBinders;
using GH.Northwind.Web.Models;

namespace GH.Northwind.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );*/

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Northwind", action = "AllProducts", id = UrlParameter.Optional} // Parameter defaults
                );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            NorthwindController.NorthwindSvr = NorthwindHandler.GetNorthwindService();
            System.Web.Mvc.ModelBinders.Binders.Add(typeof(CustomerWithOrderModel), new CustomerWithOrderBinder());
            System.Web.Mvc.ModelBinders.Binders.Add(typeof(AllProductsModel), new AllProductsBinder());
            System.Web.Mvc.ModelBinders.Binders.Add(typeof(SuppliersCategoriesModel), new SuppliersCategoriesBinder());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Logger.Instance.Error(exception);
        }

    }
}