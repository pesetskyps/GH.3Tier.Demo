using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GH.Common.Framework.Business;

namespace GH.Northwind.Business.Entities
{
    [MetadataType(typeof (OrderMetadata))]
    public partial class Order : BusinessEntityBase
    {
    }

    public class OrderMetadata
    {
        [DisplayName("Order")]
        [Required]
        public int OrderID { get; set; }

        [DisplayName("Customer")]
        [StringLength(5)]
        public string CustomerID { get; set; }

        [DisplayName("Employee")]
        public int? EmployeeID { get; set; }

        [DisplayName("Order Date")]
        public DateTime? OrderDate { get; set; }

        [DisplayName("Required Date")]
        public DateTime? RequiredDate { get; set; }

        [DisplayName("Shipped Date")]
        public DateTime? ShippedDate { get; set; }

        [DisplayName("Ship Via")]
        public int? ShipVia { get; set; }

        [DisplayName("Freight")]
        public decimal? Freight { get; set; }

        [DisplayName("Ship Name")]
        [StringLength(40)]
        public string ShipName { get; set; }

        [DisplayName("Ship Address")]
        [StringLength(60)]
        public string ShipAddress { get; set; }

        [DisplayName("Ship City")]
        [StringLength(15)]
        public string ShipCity { get; set; }

        [DisplayName("Ship Region")]
        [StringLength(15)]
        public string ShipRegion { get; set; }

        [DisplayName("Ship Postal Code")]
        [StringLength(10)]
        public string ShipPostalCode { get; set; }

        [DisplayName("Ship Country")]
        [StringLength(15)]
        public string ShipCountry { get; set; }


        [DisplayName("Customer")]
        public virtual Customer Customer { get; set; }

        [DisplayName("Order_ Details")]
        public virtual ICollection<Order_Detail> Order_Details { get; set; }
    }
}