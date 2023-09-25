using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvcDemo.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryTypeName { get;set; }

        public DateTime currentDate { get; set; }
    }
}