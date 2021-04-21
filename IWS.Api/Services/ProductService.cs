using IWS.Api.Models;
using IWS.Resource;
using IWS.Resource.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWS.Api.Services
{
    public class ProductService
    {
        private readonly DataContext db;

        public ProductService(DataContext db)
        {
            this.db = db;
        }

        public IQueryable<ProductPartitial> GetProductsList()
        {
            return db.Products.AsNoTracking().Select(p => new ProductPartitial
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await db.Products.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}
