using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMvcDemo.Models
{
    public class CategoryInsertModel
    {
        public int CategoryId { get; set; }
        
        [Required]
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public int CategoryType { get; set; } = 1;

        public int IsUpdateImage { get; set; } = 1;
    }
}