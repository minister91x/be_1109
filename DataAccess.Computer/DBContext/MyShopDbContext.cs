using DataAccess.Computer.DO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Computer.DBContext
{
    //public class MyShopDbContext : Microsoft.EntityFrameworkCore.DbContext
    //{
    //    public MyShopDbContext(DbContextOptions options) : base(options)
    //    {
    //    }
    //    protected override void OnModelCreating(ModelBuilder builder) { base.OnModelCreating(builder); }
    //    public DbSet<Product>? product { get; set; }
    //    public DbSet<Account> user { get; set; }

    //}

    public class MyShopDbContext : IdentityDbContext<IdentityUser>
    {
        public MyShopDbContext(DbContextOptions<MyShopDbContext> options) :
            base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Product>? product { get; set; }
        public DbSet<Account> user { get; set; }
        public DbSet<function> function { get; set; }
        public DbSet<userfunction> userfunction { get; set; }
        public DbSet<Customer> customer { get; set; }
    }

}
