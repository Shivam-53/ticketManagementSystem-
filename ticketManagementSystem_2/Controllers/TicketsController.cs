using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ticketManagementSystem_2.Models;
using ticketManagementSystem_2.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace ticketManagementSystem_2.Controllers
{
    [Route("api/tickets")]
    [ApiController]  
    public class TicketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        //api/tickets (example)
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _context.Tickets.ToListAsync();
            return Ok(tickets);
        }

        
        [HttpGet("{id}")]
        //api/tickets/1 (example)
        public async Task<IActionResult> GetTicketById(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound(new { message = "Ticket not found" });

            return Ok(ticket);
        }

        
        [HttpPost]
        // api/tickets (example)
        public async Task<IActionResult> CreateTicket([FromBody] Ticket ticket)
        {
            if (ticket == null || string.IsNullOrEmpty(ticket.Title))
                return BadRequest(new { message = "Invalid ticket data" });

            ticket.Status = "Open"; 
            ticket.CreatedAt = System.DateTime.UtcNow;

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, ticket);
        }

        public class UpdateStatusRequest
        {
            public string NewStatus { get; set; }
        }

        [HttpPatch("{id}/edit")]
        //api/tickets/2/edit (example)
        public async Task<IActionResult> UpdateTicketStatus(int id, [FromBody] UpdateStatusRequest request)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound(new { message = "Ticket not found." });
            }

            ticket.Status = request.NewStatus;

            await _context.SaveChangesAsync();
            return Ok(ticket);
        }



        
        [HttpDelete("{id}")]
        //  api/tickets/{id}
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound(new { message = "Ticket not found" });

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Ticket deleted successfully" });
        }
    }
}
