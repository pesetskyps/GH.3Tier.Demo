using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GH.Common.Framework.Business;

namespace GH.Northwind.Business.Entities
{
    [MetadataType(typeof (CustomerMetadata))]
    public partial class Customer : BusinessEntityBase
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Address == String.Empty && Phone == String.Empty)
            {
                yield return
                    new ValidationResult("Address and phone cannot be empty in the same time.",
                                         new[] {"Address", "Phone"});
            }
            else
            {
                yield break;
            }
        }
    }

    public class CustomerMetadata
    {
        [DisplayName("Customer")]
        [Required]
        [StringLength(5)]
        public string CustomerID { get; set; }

        [DisplayName("Company Name")]
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [DisplayName("Contact Name")]
        [StringLength(30)]
        public string ContactName { get; set; }

        [DisplayName("Contact Title")]
        [StringLength(30)]
        public string ContactTitle { get; set; }

        [DisplayName("Address")]
        [StringLength(60)]
        public string Address { get; set; }

        [DisplayName("City")]
        [StringLength(15)]
        public string City { get; set; }

        [DisplayName("Region")]
        [StringLength(15)]
        public string Region { get; set; }

        [DisplayName("Postal Code")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [DisplayName("Country")]
        [StringLength(15)]
        public string Country { get; set; }

        [DisplayName("Phone")]
        [StringLength(24)]
        public string Phone { get; set; }

        [DisplayName("Fax")]
        [StringLength(24)]
        public string Fax { get; set; }

        [DisplayName("Orders")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}