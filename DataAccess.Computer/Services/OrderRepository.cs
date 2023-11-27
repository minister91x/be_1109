using Dapper;
using DataAccess.Computer.Dapper;
using DataAccess.Computer.DO;
using DataAccess.Computer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Computer.Services
{
    public class OrderRepository : BaseApplicationService, IOrderRepository
    {
        public OrderRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<int> OrderDetailInsert(OrderDetail orderDetail)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@_OrderID", orderDetail.OrderID);
                parameters.Add("@_ProductID", orderDetail.ProductID);
                parameters.Add("@_Quantity", orderDetail.Quantity);
                parameters.Add("@_ResponseCode", 0, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                await DbConnectionHelper.ExecuteAsync("SP_ORDER_DETAIL_INSERT", parameters);
                var Id = Convert.ToInt32(parameters.Get<System.Int32>("@_ResponseCode").ToString());
                return Id;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> OrderInsert(Order order)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@_TotalAmount", order.TotalAmount);
                parameters.Add("@_CustomerID", order.CustomerID);
                parameters.Add("@_ResponseCode", 0, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                await DbConnectionHelper.ExecuteAsync("SP_ORDER_INSERT", parameters);
                var Id = Convert.ToInt32(parameters.Get<System.Int32>("@_ResponseCode").ToString());
                return Id;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<int> Customer_Insert(Customer customer)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@_CustomerName", customer.CustomerName);
                parameters.Add("@_CustomerEmail", customer.CustomerEmail);
                parameters.Add("@_CustomerPhoneNumber", customer.CustomerPhoneNumber);
                parameters.Add("@_CustomerAddress", customer.CustomerAddress);
                parameters.Add("@_ResponseCode", 0, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                await DbConnectionHelper.ExecuteAsync("SP_CUSTOMER_INSERT", parameters);
                var Id = Convert.ToInt32(parameters.Get<System.Int32>("@_ResponseCode").ToString());
                return Id;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
