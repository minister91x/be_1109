using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvcDemo.Models
{
    public class ShoppingCart
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int Price { get; set; }

        public int Quantity { get; set; }

    }

    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerAddress { get; set; }
       // public DateTime CreatedDate { get; set; }
    }

    public class OrderRequest
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
    public class CreateOrderRequestData : RequestData
    {
        public Customer customer { get; set; }
        public List<OrderRequest> orderItems { get; set; }


    }
}