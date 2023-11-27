using DataAccess.Computer.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Computer.IServices
{
    public interface IOrderRepository
    {
        Task<int> OrderInsert(Order order);
        Task<int> OrderDetailInsert(OrderDetail orderDetail);

        Task<int> Customer_Insert(Customer customer);
    }
}
