using CSharpEntityFrameworkPRSLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CSharpEntityFrameworkPRSLibrary {

    public class AppDbContext : DbContext {

        //define table in class after create it so can access it
        //have to do for every class to map to table
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            //this is going to add in functionality, add sql server functions and proxies
            //first time instantiated load sql service and proxy service..load if not not loaded
            if (!builder.IsConfigured) {
                //if not configured then configure
                //configure proxies
                builder.UseLazyLoadingProxies();
                var connStr = @"server = localhost\sqlexpress; database=CustEfDb; trusted_connection=true;";
                builder.UseSqlServer(connStr);//pass in connection string
                
            }
        }

    }
}
