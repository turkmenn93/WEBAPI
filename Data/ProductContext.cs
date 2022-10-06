using System;
using Microsoft.EntityFrameworkCore;

namespace UdemyWEBAPI.Data
{
    public class ProductContext: DbContext
    {
       
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
           
        }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new() {ID = 1,CategoryName = "Elektronik"},
                new() {ID = 2,CategoryName = "Giyim"}
            });


            modelBuilder.Entity<Product>().Property(x => x.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>().HasData(new Product[]
            {
                new(){ID = 1,ProductName="Bilgisayar",CreateDate = DateTime.Now.AddDays(-3),Price = 15000,Stock = 300,categoryId=1},
                new(){ID = 2,ProductName="Telefon",CreateDate = DateTime.Now.AddDays(-30),Price = 10000,Stock = 500,categoryId=1},
                new(){ID = 3,ProductName="Klavye",CreateDate = DateTime.Now.AddDays(-60),Price = 100,Stock = 1000,categoryId=2}
            });
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
