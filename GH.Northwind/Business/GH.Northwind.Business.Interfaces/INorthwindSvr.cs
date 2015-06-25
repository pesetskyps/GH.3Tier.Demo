

using System;
using System.Collections.Generic;
using System.ServiceModel;
using GH.Northwind.Business.Entities;

namespace GH.Northwind.Business.Interfaces
{
    [ServiceContract]
    public interface INorthwindSvr
    {
        [OperationContract]
        List<Customer> GetCustomers();

        [OperationContract]
        void InsertCustomer(Customer customer, bool commit);

        [OperationContract]
        void UpdateCustomer(Customer currentCustomer, bool commit);

        [OperationContract]
        void DeleteCustomer(String customerId, bool commit);

        [OperationContract]
        List<Order> GetOrders();

        [OperationContract]
        List<Order_Detail> GetOrderDetailForAnOrder(int orderId);

        [OperationContract]
        List<Order> GetOrderForACustomer(String customerId);

        [OperationContract]
        void CreateOrder(Order order, Order_Detail[] details);

        [OperationContract]
        void UpdateOrder(Order currentOrder, Order_Detail[] details, bool commit);

        [OperationContract]
        void DeleteOrder(int orderId, bool commit);

        [OperationContract]
        void DeleteAnOrderDetailFromAnOrder(int orderId, int orderDetailId, bool commit);

        [OperationContract]
        List<Product> GetProducts();

        [OperationContract]
        Product GetProductById(int id);

        [OperationContract]
        void InsertProduct(Product product, bool commit);

        [OperationContract]
        void UpdateProduct(Product currentProduct, bool commit);

        [OperationContract]
        void DeleteProduct(int productId, bool commit);

        [OperationContract]
        List<Category> GetProductCategories();

        [OperationContract]
        List<Supplier> GetSuppliers();

        [OperationContract]
        void Commit();
    }
}