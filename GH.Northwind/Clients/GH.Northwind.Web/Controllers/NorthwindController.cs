
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web.Mvc;
using GH.Common.LogService;
using GH.Northwind.Business.Entities;
using GH.Northwind.Business.Entities.Exceptions;
using GH.Northwind.Business.Interfaces;
using GH.Northwind.Web.ModelBinders;
using GH.Northwind.Web.Models;

namespace GH.Northwind.Web.Controllers
{
    public class NorthwindController : Controller
    {
        private readonly ILogger<NorthwindController> Log = Log<NorthwindController>.LogProvider;
        private static INorthwindSvr _northwindSvr;

        public static INorthwindSvr NorthwindSvr
        {
            get
            {
                if (_northwindSvr == null)
                    throw new ApplicationException("NorthwindController.NorthwindSvr: NorthwindSvr is null.");
                return _northwindSvr;
            }
            set { _northwindSvr = value; }
        }

        public ActionResult Index()
        {
            Logger.Instance.Debug("reached index controller");
            return View();
        }

        public ActionResult AllProducts(AllProductsModel allProductsModel)
        {
            try
            {
                Logger.Instance.Debug("reached AllProducts controller");
                allProductsModel.Products = NorthwindSvr.GetProducts();
                return View(allProductsModel.Products);
            }
            catch (FaultException<BusinessServiceException> e)
            {
                Log.Error("Web.NorthwindController.AllProducts(...) Error from Business service layer: " + e.Message);
                throw;
            }
            catch (FaultException ex)
            {
                Log.Error("Web.NorthwindController.AllProducts(...) Error from Business service layer: " + ex.Message);
                throw;
            }
            catch (Exception exx)
            {
                Log.Fatal("Web.NorthwindController.AllProducts(...) Error: " + exx.Message);
                throw;
            }
        }

        //
        // GET: /NorthwindSvr/Details/5

        public ActionResult DetailsProduct(int? id, AllProductsModel allProductsModel, SuppliersCategoriesModel model)
        {
            Product p = null;
            if (id != null) p = allProductsModel.ProductById(id.Value);
            else
            {
                p = allProductsModel.ProductById((int)TempData["id"]);
            }
            if (p.Supplier == null) p.Supplier = model.SupplierList.Where(s => s.SupplierID == p.SupplierID).DefaultIfEmpty(null).First();
            if (p.Category == null) p.Category = model.CategoryList.Where(c => c.CategoryID == p.CategoryID).DefaultIfEmpty(null).First();
            return View(p);
        }

       
        //
        // GET: /NorthwindSvr/Create

        public ActionResult CreateProduct(SuppliersCategoriesModel model)
        {
            ViewBag.SuppliersCategoriesModel = model;
            return View(new Product());
        }

        //
        // POST: /NorthwindSvr/Create

        [HttpPost]
        public ActionResult CreateProduct(Product p, AllProductsModel allProductsModel)
        {
            NorthwindSvr.InsertProduct(p, true);
            TempData["id"] = p.ProductID;
            allProductsModel.Products.Insert(allProductsModel.Products.Count, p);
            return RedirectToAction("DetailsProduct");
        }

        //
        // GET: /NorthwindSvr/Edit/5

        public ActionResult EditProduct(int id, AllProductsModel allProductsModel, SuppliersCategoriesModel suppliersCategoriesModel)
        {
            Product p = allProductsModel.ProductById(id);
            ViewBag.SuppliersCategoriesModel = suppliersCategoriesModel;
            return View(p);
        }

        //
        // POST: /NorthwindSvr/Edit/5

        [HttpPost]
        public ActionResult EditProduct(Product p, SuppliersCategoriesModel suppliersCategoriesModel)
        {
            NorthwindSvr.UpdateProduct(p, true);
            ViewBag.SuppliersCategoriesModel = suppliersCategoriesModel;
            return View(p); 
        }

        //
        // GET: /NorthwindSvr/Delete/5

        public ActionResult DeleteProduct(int id)
        {
            Product p = NorthwindSvr.GetProductById(id);
            return View(p);
        }

        //
        // POST: /NorthwindSvr/Delete/5

        [HttpPost]
        public ActionResult DeleteProduct(int id, FormCollection collection)
        {
            NorthwindSvr.DeleteProduct(id, true);
            return RedirectToAction("AllProducts");
        }
    }
}