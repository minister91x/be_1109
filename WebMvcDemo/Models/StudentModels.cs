using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvcDemo.Models
{
    public class StudentModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StudentModels_New
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StudentModels_TrungGian
    {
        public List<StudentModels> studentModels { get; set; }
        public List<StudentModels_New> StudentModels_New { get; set; }
        public StudentModels studentModel { get; set; }
    }
}