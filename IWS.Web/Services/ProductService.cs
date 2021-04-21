using IWS.Resource;
using IWS.Resource.Models;
using IWS.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWS.Web.Services
{
    public class ProductService
    {
        private readonly DataContext db;

        public ProductService(DataContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ProductPartitial>> GetProductsListAsync()
        {
            return await db.Products.AsNoTracking().Select(p => new ProductPartitial
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }
            ).ToListAsync();
        }

        public async Task CreateProductAsync(Product product)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            db.Products.Update(product);
            await db.SaveChangesAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await db.Products.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}
