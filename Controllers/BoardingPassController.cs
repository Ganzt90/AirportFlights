    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirportFlights.Data;
using AirportFlights.Models;
using System.Text.Json;

namespace AirportFlights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardingPassController : ControllerBase
    {
        private readonly DemoContext _context;

        public BoardingPassController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/BoardingPass
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoardingPass>>> GetBoardingPasses()
        {
          if (_context.BoardingPasses == null)
          {
              return NotFound();
          }
            return await _context.BoardingPasses.ToListAsync();
        }

        // GET: api/BoardingPass/5
        [HttpGet("{ticket_no}/{flight_id}")]
        public  IQueryable<BoardingPass> GetBoardingPass(string ticket_no, int flight_id)
        {
          if (_context.BoardingPasses == null)
          {
              return (IQueryable<BoardingPass>)NotFound();
          }
            var boardingPass = _context.BoardingPasses.Where(p => p.FlightId == flight_id && p.TicketNo == ticket_no);

            string jsonString = JsonSerializer.Serialize<IQueryable<BoardingPass>>(boardingPass);
            Console.WriteLine(jsonString);
            if (boardingPass == null)
            {
                return (IQueryable<BoardingPass>)NotFound();
            }

            return boardingPass;
        }

        // PUT: api/BoardingPass/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoardingPass(string id, BoardingPass boardingPass)
        {
            if (id != boardingPass.TicketNo)
            {
                return BadRequest();
            }

            _context.Entry(boardingPass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardingPassExists(id))
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

        // POST: api/BoardingPass
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BoardingPass>> PostBoardingPass(BoardingPass boardingPass)
        {
          if (_context.BoardingPasses == null)
          {
              return Problem("Entity set 'DemoContext.BoardingPasses'  is null.");
          }
            _context.BoardingPasses.Add(boardingPass);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BoardingPassExists(boardingPass.TicketNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBoardingPass", new { id = boardingPass.TicketNo }, boardingPass);
        }

        // DELETE: api/BoardingPass/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoardingPass(string id)
        {
            if (_context.BoardingPasses == null)
            {
                return NotFound();
            }
            var boardingPass = await _context.BoardingPasses.FindAsync(id);
            if (boardingPass == null)
            {
                return NotFound();
            }

            _context.BoardingPasses.Remove(boardingPass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoardingPassExists(string id)
        {
            return (_context.BoardingPasses?.Any(e => e.TicketNo == id)).GetValueOrDefault();
        }
    }
}
