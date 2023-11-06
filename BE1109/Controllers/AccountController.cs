using DataAccess.Computer.DO;
using DataAccess.Computer.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
                var refreshToken = GenerateRefreshToken();

                // update refreshToken vào db
                var expired = _configuration["JWT:RefreshTokenValidityInDays"] ?? "";
                var result_update = _accountRepository.Account_UpdateRefeshToken(new Account_UpdateRefeshTokenRequestData
                {
                    UserID= result.UserID,
                    RefreshToken= refreshToken,
                    RefreshTokenExpiryTime= DateTime.Now.AddDays(Convert.ToInt32(expired))
                });

                returnData.Code = 1;
                returnData.token = token;
                returnData.refeshToken = refreshToken;
                return Ok(returnData);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string username = principal.Identity.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            //var user = await _userManager.FindByNameAsync(username);

            //if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            //{
            //    return BadRequest("Invalid access token or refresh token");
            //}

            //var newAccessToken = CreateToken(principal.Claims.ToList());
            //var newRefreshToken = GenerateRefreshToken();

            //user.RefreshToken = newRefreshToken;
            //await _userManager.UpdateAsync(user);

            //return new ObjectResult(new
            //{
            //    accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            //    ref
            //
            //    reshToken = newRefreshToken
            //});

            return Ok();
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
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
    }
}
