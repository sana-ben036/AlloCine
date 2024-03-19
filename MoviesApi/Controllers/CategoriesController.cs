using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet] 
        public async Task<IActionResult> GetAllAsync() 
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories.OrderBy(c => c.Name));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CategoryDto dto)
        {
            var category = new Category { Name = dto.Name };

            await _context.AddAsync(category);

            _context.SaveChanges();


            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(byte id, [FromBody] CategoryDto dto)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound($"No category was found with ID: {id}");

            category.Name = dto.Name;

            _context.SaveChanges();


            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound($"No category was found with ID: {id}");

            _context.Remove(category);

            _context.SaveChanges();


            return Ok($"{category.Name} category is deleted");
        }







    }
}
