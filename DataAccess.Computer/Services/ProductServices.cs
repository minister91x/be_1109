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
    public class ProductServices : IProductServices
    {
        MyShopDbContext _dbContext;

        public ProductServices(MyShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetProducts()
        {
            var list = new List<Product>();
            try
            {
                //for (int i = 10; i < 15; i++)
                //{
                //    list.Add(new Product { Id = i, Name = "Iphone " + i });
                //}

                list =  _dbContext.product.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }

            return list;
        }

        public async Task<int> ProductInsert(Product product)
        {
            var result = 0;
            try
            {
                _dbContext.product.Add(product);
                return _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

            return result;
        }
    }
}
