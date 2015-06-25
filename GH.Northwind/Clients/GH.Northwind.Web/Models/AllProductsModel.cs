using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GH.Northwind.Business.Entities;

namespace GH.Northwind.Web.Models
{
    public class AllProductsModel
    {
        public List<Product> Products { get; set; }
        public Product ProductById(int id)
        {
            return Products.Where(p => p.ProductID == id).First();
        }
    }
}