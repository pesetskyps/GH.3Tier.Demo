using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GH.Common.Framework.Business;

namespace GH.Northwind.Business.Entities
{
    [MetadataType(typeof (ProductMetadata))]
    public partial class Product : BusinessEntityBase
    {
    }

    public class ProductMetadata
    {
        [DisplayName("Product")]
        [Required]
        public int ProductID { get; set; }

        [DisplayName("Product Name")]
        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        [DisplayName("Supplier")]
        public int? SupplierID { get; set; }

        [DisplayName("Category")]
        public int? CategoryID { get; set; }

        [DisplayName("Quantity Per Unit")]
        [StringLength(20)]
        public string QuantityPerUnit { get; set; }

        [DisplayName("Unit Price")]
        public decimal? UnitPrice { get; set; }

        [DisplayName("Units In Stock")]
        public short? UnitsInStock { get; set; }

        [DisplayName("Units On Order")]
        public short? UnitsOnOrder { get; set; }

        [DisplayName("Reorder Level")]
        public short? ReorderLevel { get; set; }

        [DisplayName("Discontinued")]
        [Required]
        public bool Discontinued { get; set; }


        [DisplayName("Order_ Details")]
        public virtual ICollection<Order_Detail> Order_Details { get; set; }
    }
}