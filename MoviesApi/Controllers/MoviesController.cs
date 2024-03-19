using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Dtos;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(MovieDto dto)
        {

            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);
            var movie = new Movie 
            { CategoryId = dto.CategoryId,
             Title = dto.Title,
             Poster = dataStream.ToArray(),
             Rate = dto.Rate,
             Year = dto.Year,
             Storyline = dto.Storyline
           
            };

            await _context.AddAsync(movie);

            _context.SaveChanges();
            return Ok(movie);
        }
    }
}
