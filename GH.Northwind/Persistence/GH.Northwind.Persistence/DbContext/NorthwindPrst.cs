using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using GH.Common.Utils;
using GH.Common.Framework.Persistence.DataServiceContext;
using GH.Northwind.Common.Entities;
using GH.Common.Framework.Persistence; 


namespace GH.ProductRegistration.Persistence.DataServiceContext
{
    public class ProductRegPrst : PersistenceBase<Customer>
    {
        override protected IQueryable<Customer> EntitySet
        { get { return null; /* return (DataContext as HelperOnly.ds.GHProductRegEntities).Customers; */} }

        override protected String EntitySetName
        { get { return _entitySetName ?? (_entitySetName = Util.GetMemberNameExtra((HelperOnly.ds.GHProductRegEntities g) => g.Customers)); } }
    }

    public class ProductPrst : PersistenceBase<Product>
    {
        override protected IQueryable<Product> EntitySet
        { get { return null; /*return (DataContext as HelperOnly.ds.GHProductRegEntities).Productes;*/ } }

        override protected String EntitySetName
        { get { return _entitySetName ?? (_entitySetName = Util.GetMemberNameExtra((HelperOnly.ds.GHProductRegEntities g) => g.Productes)); } }
    }

    public class OrderPrst : PersistenceBase<Order>
    {
        override protected IQueryable<Order> EntitySet
        { get { return null; /*return (DataContext as HelperOnly.ds.GHProductRegEntities).Orders;*/ } }

        override protected String EntitySetName
        { get { return _entitySetName ?? (_entitySetName = Util.GetMemberNameExtra((HelperOnly.ds.GHProductRegEntities g) => g.Orders)); } }
    }

    public class Order_DetailPrst : PersistenceBase<Order_Detail>
    {
        override protected IQueryable<Order_Detail> EntitySet
        { get { return null; /*return (DataContext as HelperOnly.ds.GHProductRegEntities).Order_Detailes; */} }

        override protected String EntitySetName
        { get { return _entitySetName ?? (_entitySetName = Util.GetMemberNameExtra((HelperOnly.ds.GHProductRegEntities g) => g.Order_Detailes)); } }
    }
}



