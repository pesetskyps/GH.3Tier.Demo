using System.Collections.Generic;
using GH.Northwind.Business.Entities;

namespace GH.Northwind.Web.Models
{
    public class CustomerWithOrderModel
    {
        public CustomerWithOrderModel()
        {
            OrderDetails = new Queue<Order_Detail>();
        }

        public Customer Customer { get; set; }
        public Order Order { get; set; }
        public Queue<Order_Detail> OrderDetails { get; set; }
    }
}