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
        private new List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _context.Movies
                .OrderByDescending(x => x.Rate)
                .Include(c => c.Category)
                .Select(m => new MovieDetailsDto
                {
                    Id = m.Id,
                    CategoryId = m.CategoryId,
                    CategoryName = m.Category.Name,
                    Title = m.Title,
                    Poster = m.Poster,
                    Rate = m.Rate,
                    Year = m.Year,
                    Storyline = m.Storyline

                })
                .ToListAsync();

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = await _context.Movies.Include(c => c.Category).SingleOrDefaultAsync(m => m.Id == id);

            if (movie == null)
                return NotFound();
            var dto = new MovieDetailsDto
            {
                Id = movie.Id,
                CategoryId = movie.CategoryId,
                CategoryName = movie.Category.Name,
                Title = movie.Title,
                Poster = movie.Poster,
                Rate = movie.Rate,
                Year = movie.Year,
                Storyline = movie.Storyline
            };
                
            return Ok(dto);
        }

        [HttpGet ("GetByCategoryId")]
        public async Task<IActionResult> GetByCategoryIdAsync(byte categoryId)
        {
            var movies = await _context.Movies
                .Where(m  => m.CategoryId == categoryId)
                .OrderByDescending(x => x.Rate)
                .Include(c => c.Category)
                .Select(m => new MovieDetailsDto
                {
                    Id = m.Id,
                    CategoryId = m.CategoryId,
                    CategoryName = m.Category.Name,
                    Title = m.Title,
                    Poster = m.Poster,
                    Rate = m.Rate,
                    Year = m.Year,
                    Storyline = m.Storyline

                })
                .ToListAsync();

            return Ok(movies);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieDto dto)
        {
            if (dto.Poster == null)
                return BadRequest("Poster is required!");
            if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .png and .jpg images are allowed!");

            if (dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed size for poster is 1MB!");

            var isValidCategory = await _context.Categories.AnyAsync(c=>c.Id == dto.CategoryId);

            if (!isValidCategory)
                return BadRequest("Invalid Category ID!");


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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] MovieDto dto)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
                return NotFound($"No movie was found with ID: {id}");

            var isValidCategory = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId);

            if (!isValidCategory)
                return BadRequest("Invalid Category ID!");

            if(dto.Poster != null)
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg images are allowed!");

                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max allowed size for poster is 1MB!");

                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);
                movie.Poster = dataStream.ToArray();
            }

            movie.Title = dto.Title;
            movie.Rate = dto.Rate;
            movie.Year = dto.Year;
            movie.Storyline = dto.Storyline;    
            movie.CategoryId = dto.CategoryId;

            _context.SaveChanges();

            return Ok(movie);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
                return NotFound($"No movie was found with ID: {id}");

            _context.Remove(movie);

            _context.SaveChanges();


            return Ok($"{movie.Title} movie is deleted");
        }
    }
}
