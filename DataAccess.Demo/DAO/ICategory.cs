using DataAccess.Demo.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Demo.DAO
{
    public interface ICategory
    {
        List<Category> GetProducts();
        int ProductInsertOrUpdate(Category product);
        int ProductDelete(int ProductId);
    }
}
