using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GH.Common.Framework.Business;

namespace GH.Northwind.Business.Entities
{
    [MetadataType(typeof (Order_DetailMetadata))]
    public partial class Order_Detail : BusinessEntityBase
    {
    }

    public class Order_DetailMetadata
    {
        [DisplayName("Order")]
        [Required]
        public int OrderID { get; set; }

        [DisplayName("Product")]
        [Required]
        public int ProductID { get; set; }

        [DisplayName("Unit Price")]
        [Required]
        public decimal UnitPrice { get; set; }

        [DisplayName("Quantity")]
        [Required]
        public short Quantity { get; set; }

        [DisplayName("Discount")]
        [Required]
        public float Discount { get; set; }


        [DisplayName("Order")]
        public virtual Order Order { get; set; }

        [DisplayName("Product")]
        public virtual Product Product { get; set; }
    }
}