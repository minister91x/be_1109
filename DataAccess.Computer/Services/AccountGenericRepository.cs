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
    public class AccountGenericRepository : GenericRepository<Account>, IAccountGenericRepository
    {
        public AccountGenericRepository(MyShopDbContext dbContext) : base(dbContext)
        {
        }
    }
}
