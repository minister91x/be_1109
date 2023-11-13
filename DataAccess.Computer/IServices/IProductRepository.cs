using DataAccess.Computer.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Computer.IServices
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
       Task<List<Product>> GetProducts_Dapper();
        Task<int> ProductInsert(Product product);
        Task<int> ProductInsert_Dapper(Product product);
    }
}
