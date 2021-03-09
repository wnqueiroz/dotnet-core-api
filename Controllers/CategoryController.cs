using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using csharp_api.Data;
using csharp_api.Models;
using System.Linq;

namespace csharp_api.Controllers
{
    [ApiController]
    [Route("v1/categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
        {
            var categories = await context.Categories.ToListAsync();

            return categories;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post([FromServices] DataContext context, [FromBody] Category model)
        {
            if (ModelState.IsValid)
            {
                context.Categories.Add(model);
                await context.SaveChangesAsync();
                return model;

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [Route("{id:int}/products")]
        public async Task<ActionResult<List<Product>>> GetProductsByCategoryId([FromServices] DataContext context, int id)
        {
            var products = await context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p => p.CategoryId == id).ToListAsync();

            return products;
        }
    }
}