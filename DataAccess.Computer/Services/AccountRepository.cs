using DataAccess.Computer.DBContext;
using DataAccess.Computer.DO;
using DataAccess.Computer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Computer.Services
{
    public class AccountRepository : IAccountRepository
    {
        MyShopDbContext _dbContext;

        public AccountRepository(MyShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Account_UpdateRefeshToken(Account_UpdateRefeshTokenRequestData requestData)
        {
            try
            {
                var user = _dbContext.user.ToList().FindAll(s => s.UserID == requestData.UserID).FirstOrDefault();
                if (user == null || user.UserID == 0)
                {
                    return -1;
                }
                user.RefreshToken = requestData.RefreshToken;
                user.RefreshTokenExpiryTime = requestData.RefreshTokenExpiryTime;
                _dbContext.user.Update(user);
                return _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Account> Login(AccountLoginRequestData requestData)
        {
            try
            {
                // Kiểm tra xem có user nào trong DB trùng thông tin UserName/Passs do nguowif dungf truyen vao
                var account = _dbContext.user.ToList().FindAll(
                    s => s.UserName == requestData.UserName &&
                s.PassWord == requestData.PassWord).FirstOrDefault();

                // Nếu không có thì trrar về user trống
                if (account == null) { return new Account(); }

                // Nếu có thì trả về thông tin user theo UserName/Passs truyền vào 
                return account;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
