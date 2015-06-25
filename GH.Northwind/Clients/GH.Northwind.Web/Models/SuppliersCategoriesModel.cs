using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GH.Northwind.Business.Entities;
using GH.Northwind.Web.Controllers;

namespace GH.Northwind.Web.Models
{
    public class SuppliersCategoriesModel
    {
        private SelectList _suppliers = null;
        private SelectList _categories = null;
        private List<Supplier> _supplierList = null;
        private List<Category> _categoryList = null;
        public List<Supplier> SupplierList { 
            get
            {
                if (_supplierList == null) _supplierList = NorthwindController.NorthwindSvr.GetSuppliers();
                return _supplierList;
            }
        }
        public List<Category> CategoryList
        {
            get
            {
                if (_categoryList == null) _categoryList = NorthwindController.NorthwindSvr.GetProductCategories();
                return _categoryList;
            }            
        }
  
        public SelectList Suppliers { 
            get
            {
                if(_suppliers == null)
                {
                    IEnumerable<SelectListItem> selectListSuppliers =
                    from c in SupplierList
                    select new SelectListItem
                    {
                        Selected = false,
                        Text = c.CompanyName,
                        Value = c.SupplierID.ToString()
                    };
                    _suppliers = new SelectList(selectListSuppliers, "Value", "Text");                     
                }
                return _suppliers;
            }
            set { _suppliers = value; }
        }


        public SelectList Categories
        {
            get
            {
                if(_categories == null )
                {
                    IEnumerable<SelectListItem> selectListCategory =
                    from c in CategoryList
                    select new SelectListItem
                    {
                        Selected = false,
                        Text = c.CategoryName,
                        Value = c.CategoryID.ToString()
                    };
                    _categories = new SelectList(selectListCategory, "Value", "Text");   
                }
                return _categories;
            }
            set { _categories = value; } 
        }
    }
}