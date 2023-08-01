using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirportFlights.Data;
using AirportFlights.Models;

namespace AirportFlights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly DemoContext _context;

        public SeatController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/Seat
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seat>>> GetSeats()
        {
          if (_context.Seats == null)
          {
              return NotFound();
          }
            return await _context.Seats.ToListAsync();
        }

        // GET: api/Seat/5
        [HttpGet("{aircraft_code}/{seat_no}")]
        public IQueryable<Seat> GetSeat(string aircraft_code,string seat_no)
        {
          if (_context.Seats == null)
          {
              return (IQueryable<Seat>)NotFound();
          }
            IQueryable<Seat> seat =  _context.Seats.Where(p => p.AircraftCode == aircraft_code && p.SeatNo == seat_no);

            if (seat == null)
            {
                return (IQueryable<Seat>)NotFound();
            }

            return seat;
        }

        // PUT: api/Seat/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeat(string id, Seat seat)
        {
            if (id != seat.AircraftCode)
            {
                return BadRequest();
            }

            _context.Entry(seat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeatExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Seat
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Seat>> PostSeat(Seat seat)
        {
          if (_context.Seats == null)
          {
              return Problem("Entity set 'DemoContext.Seats'  is null.");
          }
            _context.Seats.Add(seat);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SeatExists(seat.AircraftCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSeat", new { id = seat.AircraftCode }, seat);
        }

        // DELETE: api/Seat/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeat(string id)
        {
            if (_context.Seats == null)
            {
                return NotFound();
            }
            var seat = await _context.Seats.FindAsync(id);
            if (seat == null)
            {
                return NotFound();
            }

            _context.Seats.Remove(seat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeatExists(string id)
        {
            return (_context.Seats?.Any(e => e.AircraftCode == id)).GetValueOrDefault();
        }
    }
}
