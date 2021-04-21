using IWS.Api.Models;
using IWS.Api.Services;
using IWS.Resource.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWS.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService product;

        public ProductController(ProductService product)
        {
            this.product = product;
        }


        [HttpGet]
        public async Task<IEnumerable<ProductPartitial>> ProductList(string name, int? minPrice, int? maxPrice)
        {
            IQueryable<ProductPartitial> result = product.GetProductsList();

            if (!string.IsNullOrEmpty(name))
            {
                result = result.Where(p => p.Name == name);
            }

            if (!(minPrice is null))
            {
                result = result.Where(p => p.Price >= minPrice);
            }

            if (!(maxPrice is null))
            {
                result = result.Where(p => p.Price <= maxPrice);
            }

            return await result.ToListAsync();
        }

        [HttpGet]
        public async Task<Product> Product(int id)
        {
            return await product.GetProductAsync(id);
        }
    }
}
