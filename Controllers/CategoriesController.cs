using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UdemyWEBAPI.Data;

namespace UdemyWEBAPI.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ProductContext _context;
        public CategoriesController(ProductContext context)
        {
            _context = context; 
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetWithProducts(int id)
        {
            var data = await _context.Categories.Include(x => x.Products).SingleOrDefaultAsync(x => x.ID == id);
               
            if(data == null)    
                return NotFound();
            return Ok(data);

        }


    }
}
