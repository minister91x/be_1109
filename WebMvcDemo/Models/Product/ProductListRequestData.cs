using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvcDemo.Models
{
    public class ProductListRequestData: RequestData
    {
    }

    public class RequestData
    {
        public string token { get; set; }
    }
}