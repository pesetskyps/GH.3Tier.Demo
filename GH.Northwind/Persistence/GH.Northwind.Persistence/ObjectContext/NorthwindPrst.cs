using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using GH.Common.Utils; 
using GH.Common.Framework.Persistence.ObjectContext;
using GH.Northwind.Common.Entities; 
using GH.Northwind.EntityFramework;


namespace GH.ProductRegistration.Persistence.ObjectContext
{
    public class CustomerPrst : PersistenceBase<Customer>
    {
        override protected IQueryable<Customer> EntitySet
        { get { return (DataContext as NorthwindEntities).Customers;} }

        override protected String EntitySetName
        { get { return _entitySetName ?? (_entitySetName = Util.GetMemberNameExtra((NorthwindEntities g) => g.Customers)); } }
    }

    public class ProductPrst : PersistenceBase<Product>
    {
        override protected IQueryable<Product> EntitySet
        { get { return (DataContext as NorthwindEntities).Products; } }

        override protected String EntitySetName
        { get { return _entitySetName ?? (_entitySetName = Util.GetMemberNameExtra((NorthwindEntities g) => g.Products)); } }
    }

    public class OrderPrst : PersistenceBase<Order>
    {
        override protected IQueryable<Order> EntitySet
        { get { return (DataContext as NorthwindEntities).Orders; } }

        override protected String EntitySetName
        { get { return _entitySetName ?? (_entitySetName = Util.GetMemberNameExtra((NorthwindEntities g) => g.Orders)); } }
    }

    public class Order_DetailPrst : PersistenceBase<Order_Detail>
    {
        override protected IQueryable<Order_Detail> EntitySet
        { get { return (DataContext as NorthwindEntities).Order_Details; } }

        override protected String EntitySetName
        { get { return _entitySetName ?? (_entitySetName = Util.GetMemberNameExtra((NorthwindEntities g) => g.Order_Details)); } }
    }
}



