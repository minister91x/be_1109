using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Computer.DO
{
    public class Order
    {
        public int Id { get; set; }
        public long TotalAmount { get; set; }

        public int CustomerID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UpdateUser { get; set; }
    }


    public class OrderRequest
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
    public class CreateOrderRequestData
    {
        public Customer customer { get; set; }
        public List<OrderRequest> orderItems { get; set; }


    }
}
