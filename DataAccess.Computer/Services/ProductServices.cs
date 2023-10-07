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
        //public ProductServices(string a, int b, string c)
        //{

        //}

        public async Task<List<Product>> GetProducts()
        {
            var list = new List<Product>();
            try
            {
                for (int i = 10; i < 15; i++)
                {
                    list.Add(new Product { Id = i, Name = "Iphone " + i });
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return list;
        }

        public Task<int> ProductInsert(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
