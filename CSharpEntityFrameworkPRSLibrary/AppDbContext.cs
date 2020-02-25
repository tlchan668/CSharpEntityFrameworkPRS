using CSharpEntityFrameworkPRSLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CSharpEntityFrameworkPRSLibrary {

    public class AppDbContext : DbContext {

        //define table in class after create it so can access it
        //have to do for every class to map to table
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Orderline> Orderlines { get; set; }

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
        
        protected override void OnModelCreating(ModelBuilder model) {
            model.Entity<Product>(e => {
                e.ToTable("Products");
                e.HasKey(x => x.Id);
                e.Property(x => x.Code).HasMaxLength(10).IsRequired();
                e.Property(x => x.Name).HasMaxLength(30).IsRequired();
                e.Property(x => x.Price);
                e.HasIndex(x => x.Code).IsUnique();
            });
            model.Entity<Orderline>(e => {
                e.ToTable("Orderlines");
                e.HasKey(x => x.Id);
                e.HasOne(x => x.Product).WithMany(x => x.Orderlines)
                                        .HasForeignKey(x => x.ProductId)
                                        .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
