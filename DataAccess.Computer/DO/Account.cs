using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Computer.DO
{
    public class Account
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string FullName { get; set; }
        public int IsAdmin { get; set; }

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }

    public class AccountLoginRequestData
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
    public class TokenModel
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
    public class Account_UpdateRefeshTokenRequestData
    {
        public int UserID { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
    public class AccountLoginResponseData : ReturnData
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public int IsAdmin { get; set; }

    }

    public class ReturnData
    {
        public int Code { get; set; }
        public string token { get; set; }
        public string refeshToken { get; set; }
        public int ResponseCode { get; set; }
        public string Desciption { get; set; }
    }

    public class MediaReturnData
    {
        public int ResponseCode { get; set; }
        public string Description { get; set; }
    }
}
