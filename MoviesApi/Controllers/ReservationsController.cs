using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Dtos;
using MoviesApi.Models;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var reservations = await _context.Reservations
                .OrderByDescending(x => x.Date)
                .Include(e => e.Event)
                .Select(r => new ReservationDetailsDto
                {
                    Id = r.Id,
                    EventId = r.EventId,
                    DateEvent = r.Event.Date,
                    StatutId = r.Statut.Id,
                    StatutLabel = r.Statut.Label,
                    Nb_Ticket = r.Nb_Ticket,
                    Date = r.Date

                })
                .ToListAsync();

            return Ok(reservations);
        }

        [HttpGet("GetByStatutId")]
        public async Task<IActionResult> GetByEventIdAsync(int statutId)
        {
            var reservations = await _context.Reservations
                .Where(r => r.StatutId == statutId)
                .OrderByDescending(x => x.Date)
                .Select(r => new ReservationDetailsDto
                {
                    Id = r.Id,
                    StatutId = r.Statut.Id,
                    StatutLabel = r.Statut.Label,
                    Date = r.Date,
                    EventId = r.Event.Id,
                    DateEvent = r.Event.Date,
                    Nb_Ticket = r.Nb_Ticket

                })
                .ToListAsync();

            return Ok(reservations);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] ReservationDto dto)
        {
            var evt = await _context.Events.SingleOrDefaultAsync(e => e.Id == dto.EventId);

            if (evt == null)
                return NotFound($" Event was not found with ID: {dto.EventId}");

            if (dto.Nb_Ticket > evt.Ticket)
                return BadRequest("Insufficient of tickets");

            var DefaultStatut = await _context.Status.SingleOrDefaultAsync(s => s.Id == 3);


            var reservation = new Reservation
            {
                EventId = dto.EventId,
                Statut = DefaultStatut,
                Date = DateTime.Now,
                Nb_Ticket = dto.Nb_Ticket

            };

            evt.Ticket = evt.Ticket - reservation.Nb_Ticket; ;

            await _context.Reservations.AddAsync(reservation);

            _context.SaveChanges();
            return Ok(reservation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] ReservationUpdateDto dto)
        {
            var res = await _context.Reservations.FindAsync(id);

            if (res == null)
                return NotFound($" No Reseravtion was found with ID: {id}");

       
            
            if(dto.StatutId == 2)
            {
                res.StatutId = dto.StatutId;
                var e = await _context.Events.SingleOrDefaultAsync(e => e.Id == res.EventId);
                e.Ticket = e.Ticket + res.Nb_Ticket;


            }

            _context.SaveChanges();
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var res = await _context.Reservations.FindAsync(id);

            if (res == null)
                return NotFound($"No Reservation was found with ID: {id}");

            _context.Remove(res);

            _context.SaveChanges();


            return Ok($"The Reservation ID: {id} is deleted");
        }








    }













}
