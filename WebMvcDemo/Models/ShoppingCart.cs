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
}