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
    public class TicketVController : ControllerBase
    {
        private readonly DemoContext _context;

        public TicketVController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/TicketV
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
          if (_context.Tickets == null)
          {
              return NotFound();
          }
            return await _context.Tickets.ToListAsync();
        }

        // GET: api/TicketV/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(string id)
        {
          if (_context.Tickets == null)
          {
              return NotFound();
          }
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/TicketV/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(string id, Ticket ticket)
        {
            if (id != ticket.TicketNo)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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

        // POST: api/TicketV
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
          if (_context.Tickets == null)
          {
              return Problem("Entity set 'DemoContext.Tickets'  is null.");
          }
            _context.Tickets.Add(ticket);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TicketExists(ticket.TicketNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTicket", new { id = ticket.TicketNo }, ticket);
        }

        // DELETE: api/TicketV/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(string id)
        {
            if (_context.Tickets == null)
            {
                return NotFound();
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(string id)
        {
            return (_context.Tickets?.Any(e => e.TicketNo == id)).GetValueOrDefault();
        }
    }
}
