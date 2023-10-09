using DataAccess.Computer.DO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Computer.DBContext
{
    public class MyShopDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public MyShopDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder) { base.OnModelCreating(builder); }
        public DbSet<Product>? product { get; set; }

    }
}
