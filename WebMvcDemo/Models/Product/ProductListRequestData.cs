using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvcDemo.Models
{
    public class ProductListRequestData: RequestData
    {
    }


    public class ProductInsertRequestData
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string DonViTinh { get; set; }
        public int DonGia { get; set; }
        public string base64Image { get; set; }
        public int ImageChange { get; set; }
    }
    public class RequestData
    {
        public string token { get; set; }
    }
}