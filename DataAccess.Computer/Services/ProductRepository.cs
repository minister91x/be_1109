using Dapper;
using DataAccess.Computer.Dapper;
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
    public class ProductServices : BaseApplicationService, IProductRepository
    {
        MyShopDbContext _dbContext;

        public ProductServices(IServiceProvider serviceProvider, MyShopDbContext dbContext) : base(serviceProvider)
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

                list = _dbContext.product.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }

            return list;
        }
        public async Task<List<Product>> GetProducts_Dapper()
        {
            var list = new List<Product>();
            try
            {
                list = await DbConnectionHelper.QueryAsync<Product>("SP_Product_GetList");
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

        public async Task<int> ProductInsert_Dapper(Product product)
        {
            var result = 0;

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@_ProductName", product.ProductName);
                parameters.Add("@_DonViTinh", product.DonViTinh);
                parameters.Add("@_DonGia", product.DonGia);
                parameters.Add("@_ResponseCode", 0, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                await DbConnectionHelper.ExecuteAsync("SP_ProductInsert", parameters);

                var Id = Convert.ToInt32(parameters.Get<System.Int32>("@_ResponseCode").ToString());
                return Id;

            }
            catch (Exception ex)
            {

                throw;
            }


            return result;
        }
    }
}
