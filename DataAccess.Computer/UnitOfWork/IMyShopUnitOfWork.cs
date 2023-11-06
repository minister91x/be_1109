using DataAccess.Computer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Computer.UnitOfWork
{
    public interface IMyShopUnitOfWork
    {
        //IProductRepository _productRepository { get; }
        //IAccountRepository _accountRepository { get; }

        IAccountGenericRepository _accountGenericRepository { get; }
        IProductGenericRepository _productGenericRepository { get; }
        int SaveChange();

    }
}
