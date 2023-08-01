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
    public class TicketFlightController : ControllerBase
    {
        private readonly DemoContext _context;

        public TicketFlightController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/TicketFlight
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketFlight>>> GetTicketFlights()
        {
          if (_context.TicketFlights == null)
          {
              return NotFound();
          }
            return await _context.TicketFlights.ToListAsync();
        }

        // GET: api/TicketFlight/5
        [HttpGet("{ticket_no}/{flight_id}")]
        public IQueryable<TicketFlight> GetTicketFlight(string ticket_no, int flight_id)
        {
            if (_context.TicketFlights == null)
            {
                return (IQueryable<TicketFlight>)NotFound();
            }
            var ticketFlight = _context.TicketFlights.Where(p => p.TicketNo == ticket_no && p.FlightId == flight_id);

            if (ticketFlight == null)
            {
                return (IQueryable<TicketFlight>)NotFound();
            }

            return ticketFlight;
        }

        // PUT: api/TicketFlight/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketFlight(string id, TicketFlight ticketFlight)
        {
            if (id != ticketFlight.TicketNo)
            {
                return BadRequest();
            }

            _context.Entry(ticketFlight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketFlightExists(id))
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

        // POST: api/TicketFlight
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TicketFlight>> PostTicketFlight(TicketFlight ticketFlight)
        {
          if (_context.TicketFlights == null)
          {
              return Problem("Entity set 'DemoContext.TicketFlights'  is null.");
          }
            _context.TicketFlights.Add(ticketFlight);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TicketFlightExists(ticketFlight.TicketNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTicketFlight", new { id = ticketFlight.TicketNo }, ticketFlight);
        }

        // DELETE: api/TicketFlight/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketFlight(string id)
        {
            if (_context.TicketFlights == null)
            {
                return NotFound();
            }
            var ticketFlight = await _context.TicketFlights.FindAsync(id);
            if (ticketFlight == null)
            {
                return NotFound();
            }

            _context.TicketFlights.Remove(ticketFlight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketFlightExists(string id)
        {
            return (_context.TicketFlights?.Any(e => e.TicketNo == id)).GetValueOrDefault();
        }
    }
}
