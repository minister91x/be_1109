using DataAccess.Computer.DBContext;
using DataAccess.Computer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Computer.UnitOfWork
{
    public class MyShopUnitOfWork : IMyShopUnitOfWork
    {
        // private MyShopDbContext _dbContext;
        //public IProductRepository _productRepository { get; }
        //public IAccountRepository _accountRepository { get; }

        //public MyShopUnitOfWork(MyShopDbContext dbContext , IProductRepository productRepository
        //    , IAccountRepository accountRepositor)
        //{
        //    _dbContext= dbContext;
        //    _productRepository= productRepository;
        //    _accountRepository= accountRepositor;
        //}


        private MyShopDbContext _dbContext;

        IAccountGenericRepository _accountGenericRepository { get; }
        IProductGenericRepository _productGenericRepository { get; }

        public MyShopUnitOfWork(MyShopDbContext dbContext,
            IAccountGenericRepository accountGenericRepository,
            IProductGenericRepository productGenericRepository)
        {
            _dbContext = dbContext;
            _accountGenericRepository = accountGenericRepository;
            _productGenericRepository = productGenericRepository;
        }

        public int SaveChange()
        {
           return _dbContext.SaveChanges();
        }
    }
}
