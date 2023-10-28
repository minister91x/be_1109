using BE1109.Models;
using DataAccess.Computer.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;

namespace BE1109.FilterAtribute
{
    public class MyShopAuthorizeAttribute : TypeFilterAttribute
    {
        public MyShopAuthorizeAttribute(string functionCode = "DEFAULT", string permission = "VIEW") :
            base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { functionCode, permission };
        }
    }
    public class AuthorizeActionFilter : IAsyncAuthorizationFilter
    {
        private readonly MyShopDbContext _dbContext;
        private readonly string _functionCode;
        private readonly string _permission;
        public AuthorizeActionFilter(string functionCode, string permission, MyShopDbContext dbContext)
        {
            _functionCode = functionCode;
            _permission = permission;
            _dbContext = dbContext;


        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                //Lấy thông tin User từ HttpContext.User
                var user = new UserModel
                {
                    UserID = Convert.ToInt32(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value),
                    UserName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    FullName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value,
                };
                if (user.UserID <= 0)
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new
                    {
                        Code = HttpStatusCode.Unauthorized,
                        Message = "Vui lòng đăng nhập để thực hiện chức năng này "
                    });

                    return;
                }

                // Check quyền
                var function = _dbContext.function.ToList().Where(s => s.FunctionCode == _functionCode).FirstOrDefault();

                if (function == null || function.FunctionID <= 0)
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new
                    {
                        Code = HttpStatusCode.Unauthorized,
                        Message = "Dữ liệu đầu vào không hợp lệ! "
                    });

                    return;
                }

                var userFunction = _dbContext.userfunction.ToList().Where(s => s.UserID == user.UserID
                && s.FunctionID == function.FunctionID).FirstOrDefault();

                if (userFunction == null || userFunction.FunctionID <= 0)
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new
                    {
                        Code = HttpStatusCode.Unauthorized,
                        Message = "Dữ liệu đầu vào không hợp lệ! "
                    });

                    return;
                }

                if (_permission == "VIEW")
                {
                    if (userFunction.IsView == 0)
                    {
                        context.HttpContext.Response.ContentType = "application/json";
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Result = new JsonResult(new
                        {
                            Code = HttpStatusCode.Unauthorized,
                            Message = "Bạn không có quyền thực hiện chức năng này "
                        });
                    }
                }

                if (_permission == "INSERT")
                {
                    if (userFunction.IsUpdate == 0)
                    {
                        context.HttpContext.Response.ContentType = "application/json";
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Result = new JsonResult(new
                        {
                            Code = HttpStatusCode.Unauthorized,
                            Message = "Bạn không có quyền thêm dữ liệu vào DB"
                        });
                    }
                }

                return;

            }
        }
    }
}


