using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Dtos;
using MoviesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private DateTime currentDate = DateTime.Now;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var events = await _context.Events
                .OrderByDescending(x => x.Date)
                .Include(m => m.Movie)
                .Select(e => new EventDetailsDto
                {
                    Id = e.Id,
                    MovieId = e.MovieId,
                    MovieTitle = e.Movie.Title,
                    Cinema = e.Cinema,
                    City = e.City,
                    Date = e.Date

                })
                .ToListAsync();

            return Ok(events);
        }

        [HttpGet("GetByMovieId")]
        public async Task<IActionResult> GetByEventIdAsync(int movieId)
        {
            var events = await _context.Events
                .Where(e => e.MovieId == movieId)
                .OrderByDescending(x => x.Date)
                .Include(m => m.Movie)
                .Select(e => new EventDetailsDto
                {
                    Id = e.Id,
                    MovieId = e.MovieId,
                    MovieTitle = e.Movie.Title,
                    City = e.City,
                    Cinema = e.Cinema,
                    Date = e.Date,

                })
                .ToListAsync();

            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] EventDto dto)
        {

            
            if (dto.Date < currentDate)
                return BadRequest("Date is invalid!");

            var isValidMovie = await _context.Movies.AnyAsync(m => m.Id == dto.MovieId);

            if (!isValidMovie)
                return BadRequest("Invalid Movie ID!");
            
            var ev = new Event
            {
                MovieId = dto.MovieId,
                Cinema = dto.Cinema,
                City = dto.City,
                Date = dto.Date,

            };

            await _context.AddAsync(ev);

            _context.SaveChanges();
            return Ok(ev);



        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] EventDto dto)
        {
            var ev = await _context.Events.FindAsync(id);

            if (ev == null)
                return NotFound($"No event was not found with ID: {id}");

            var isValidMovie = await _context.Movies.AnyAsync(m => m.Id == dto.MovieId);

            if (!isValidMovie)
                return BadRequest("Invalid Movie ID!");

            if (dto.Date < currentDate)
                return BadRequest("Date is invalid!");


            ev.Date = dto.Date;
            ev.Cinema = dto.Cinema;
            ev.City = dto.City;
            ev.MovieId = dto.MovieId;

            _context.SaveChanges();

            return Ok(ev);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var ev = await _context.Events.FindAsync(id);

            if (ev == null)
                return NotFound($"No event was found with ID: {id}");

            _context.Remove(ev);

            _context.SaveChanges();


            return Ok($"The event of {ev.City} at {ev.Date} is deleted");
        }












    }
    


}
