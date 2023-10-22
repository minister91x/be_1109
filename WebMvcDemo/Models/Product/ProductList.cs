using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvcDemo.Models.Product
{
    public class ProductList
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public string donViTinh { get; set; }
        public int donGia { get; set; }
    }

    public class ProductListResponseData
    {
        public List<ProductList> items { get; set; }
    }
}