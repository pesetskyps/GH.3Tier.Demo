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
using System.Linq;
using GH.Common.Framework.Persistence.DataServiceCxt;
using GH.Common.Utils;
using GH.Northwind.Persistence.HelperOnly;
using Customer = GH.Northwind.Business.Entities.Customer;
using Order = GH.Northwind.Business.Entities.Order;
using Order_Detail = GH.Northwind.Business.Entities.Order_Detail;
using Product = GH.Northwind.Business.Entities.Product;
using Supplier = GH.Northwind.Business.Entities.Supplier;
using Category = GH.Northwind.Business.Entities.Category;

namespace GH.Northwind.Persistence.DataServiceCxt
{
    public class CustomerPrst : PersistenceBase<Customer>
    {
        protected override IQueryable<Customer> EntitySet
        {
            get { return DataContext.CreateQuery<Customer>(EntitySetName); }
        }

        protected override String EntitySetName
        {
            get { return _entitySetName ?? (_entitySetName = Util.GetMemberNameExtra((NorthwindEntities g) => g.Customers)); }
        }
        
        protected override Customer FindMatchedOne(Customer toBeMatched)
        {
            return EntitySet.Where(o => o.CustomerID == toBeMatched.CustomerID).ToList().DefaultIfEmpty(null).First();
        }
    }

    public class ProductPrst : PersistenceBase<Product>
    {
        protected override IQueryable<Product> EntitySet
        {
            get { return DataContext.CreateQuery<Product>(EntitySetName); }
        }

        protected override String EntitySetName
        {
            get { return _entitySetName ?? (_entitySetName = Util.GetMemberNameExtra((NorthwindEntities g) => g.Products)); }
        }

        protected override Product FindMatchedOne(Product toBeMatched) {
            return EntitySet.Where(o => o.ProductID == toBeMatched.ProductID).ToList().DefaultIfEmpty(null).First();
        }
    }

    public class OrderPrst : PersistenceBase<Order>
    {
        protected override IQueryable<Order> EntitySet
        {
            get { return DataContext.CreateQuery<Order>(EntitySetName); }
        }

        protected override String EntitySetName
        {
            get { return _entitySetName ?? (_entitySetName = Util.GetMemberNameExtra((NorthwindEntities g) => g.Orders)); }
        }

        protected override Order FindMatchedOne(Order toBeMatched)
        {
            return EntitySet.Where(o => o.OrderID == toBeMatched.OrderID).ToList().DefaultIfEmpty(null).First();
        }
    }

    public class Order_DetailPrst : PersistenceBase<Order_Detail>
    {
        protected override IQueryable<Order_Detail> EntitySet
        {
            get { return DataContext.CreateQuery<Order_Detail>(EntitySetName); }
        }

        protected override String EntitySetName
        {
            get
            {
                return _entitySetName ??
                       (_entitySetName = Util.GetMemberNameExtra((NorthwindEntities g) => g.Order_Details));
            }
        }

        protected override Order_Detail FindMatchedOne(Order_Detail toBeMatched)
        {
            return EntitySet.Where(o => o.OrderID == toBeMatched.OrderID && o.ProductID == toBeMatched.ProductID).ToList().DefaultIfEmpty(null).First();
        }
    }

    public class SupplierPrst : PersistenceBase<Supplier>
    {
        protected override IQueryable<Supplier> EntitySet
        {
            get { return DataContext.CreateQuery<Supplier>(EntitySetName); }
        }

        protected override String EntitySetName
        {
            get
            {
                return _entitySetName ??
                       (_entitySetName = Util.GetMemberNameExtra((NorthwindEntities g) => g.Suppliers));
            }
        }

        protected override Supplier FindMatchedOne(Supplier toBeMatched)
        {
            return EntitySet.Where(o => o.SupplierID == toBeMatched.SupplierID).ToList().DefaultIfEmpty(null).First();
        }
    }

    public class CategoryPrst : PersistenceBase<Category>
    {
        protected override IQueryable<Category> EntitySet
        {
            get { return DataContext.CreateQuery<Category>(EntitySetName); }
        }

        protected override String EntitySetName
        {
            get
            {
                return _entitySetName ??
                       (_entitySetName = Util.GetMemberNameExtra((NorthwindEntities g) => g.Categories));
            }
        }

        protected override Category FindMatchedOne(Category toBeMatched)
        {
            return EntitySet.Where(o => o.CategoryID == toBeMatched.CategoryID).ToList().DefaultIfEmpty(null).First();
        }
    }
}