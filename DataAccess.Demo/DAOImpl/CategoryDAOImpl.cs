using DataAccess.Demo.DAO;
using DataAccess.Demo.DO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Demo.DAOImpl
{
    public class CategoryDAOImpl : ICategory
    {
        public List<Category> GetProducts()
        {
            var list = new List<Category>();
            try
            {
                // b1 : mở connect
                var sqlconn = DBHelper.GetConnection();

                //Bước 2: sqlcommand dể thao tác với dữ liệu

                SqlCommand cmd1 = new SqlCommand("SELECT * FROM PRODDUCT WHERE NAME = --1=1 ", sqlconn);
                //cmd1.CommandType = System.Data.CommandType.Text;

                SqlCommand cmd = new SqlCommand("SP_CategoryGetList", sqlconn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@_Name", "1=1");
                // Bước 3
                var reader = cmd.ExecuteReader();
                //while (reader.Read())
                //{
                //    var category = new Category();
                //    category.CategoryId = reader["CategoryId"] != null ? Convert.ToInt32(reader["CategoryId"].ToString()) : 0;
                //    category.CategoryName = reader["CategoryName"] != null ? reader["CategoryId"].ToString() : "";
                //    list.Add(category);
                //}
                list = DataReaderMapToList<Category>(reader);

                // Bước 4:
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }


            return list;
        }
        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
        public int ProductDelete(int ProductId)
        {
            throw new NotImplementedException();
        }

        public int ProductInsertOrUpdate(Category product)
        {
            throw new NotImplementedException();
        }
    }
}
