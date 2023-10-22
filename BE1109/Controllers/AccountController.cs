using DataAccess.Computer.DO;
using DataAccess.Computer.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BE1109.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountRepository _accountRepository;
        private IConfiguration _configuration;
        public AccountController(IAccountRepository accountRepository, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
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

                var authClaims = new List<Claim> {
                      new Claim(ClaimTypes.PrimarySid, result.UserID.ToString()),
                    new Claim(ClaimTypes.Name, result.UserName),
                     new Claim(ClaimTypes.GivenName, result.FullName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), };

              

                var newAccessToken = CreateToken(authClaims);

                var token = new JwtSecurityTokenHandler().WriteToken(newAccessToken);

                returnData.Code = 1;
                returnData.Desciption = token;
                return Ok(returnData);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

    }
}
