using IWS.Resource.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWS.Resource
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var products = new Product[] {
                new Product { Id = 1, Name = "Product1", Description = "Product1 Description", Price = 220 }
            };

            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}
