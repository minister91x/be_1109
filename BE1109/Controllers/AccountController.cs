using DataAccess.Computer.DO;
using DataAccess.Computer.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE1109.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }



        [HttpPost("Login")]
        public async Task<ActionResult> Login(AccountLoginRequestData requestData)
        {
            var returnData = new AccountLoginResponseData();
            try
            {
                var result = await _accountRepository.Login(requestData);
                if (result.UserID < 0)
                {
                    returnData.Code = -1;
                    returnData.Desciption = "Đăng nhập thất bại .Kiểm tra lại user";
                    return Ok(returnData);
                }

                returnData.Code = 1;
                returnData.Desciption = "Đăng nhập thành công";
                returnData.FullName = result.FullName;
                returnData.UserID = result.UserID;
                returnData.IsAdmin = result.IsAdmin;
                return Ok(returnData);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
