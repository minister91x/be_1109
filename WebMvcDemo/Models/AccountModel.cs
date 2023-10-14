using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvcDemo.Models
{
    public class AccountModel
    {
    }

    public class AccountLoginRequestData
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }

    public class AccountLoginResponseData
    {
        public int userID { get; set; }
        public string fullName { get; set; }
        public int isAdmin { get; set; }
        public object token { get; set; }
        public int code { get; set; }
        public string desciption { get; set; }
    }
}