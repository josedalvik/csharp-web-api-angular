using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo.DAL;
using Demo.Model;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InboxesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public InboxesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Inboxes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inbox>>> GetInbox()
        {
          if (_context.Inbox == null)
          {
              return NotFound();
          }
            return await _context.Inbox.ToListAsync();
        }

        // GET: api/Inboxes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inbox>> GetInbox(string id)
        {
          if (_context.Inbox == null)
          {
              return NotFound();
          }
            var inbox = await _context.Inbox.FindAsync(id);

            if (inbox == null)
            {
                return NotFound();
            }

            return inbox;
        }

        // PUT: api/Inboxes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInbox(string id, Inbox inbox)
        {
            if (id != inbox.Id)
            {
                return BadRequest();
            }

            _context.Entry(inbox).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InboxExists(id))
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

        // POST: api/Inboxes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inbox>> PostInbox(Inbox inbox)
        {
          if (_context.Inbox == null)
          {
              return Problem("Entity set 'ApplicationDBContext.Inbox'  is null.");
          }
            _context.Inbox.Add(inbox);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInbox", new { id = inbox.Id }, inbox);
        }

        // DELETE: api/Inboxes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInbox(string id)
        {
            if (_context.Inbox == null)
            {
                return NotFound();
            }
            var inbox = await _context.Inbox.FindAsync(id);
            if (inbox == null)
            {
                return NotFound();
            }

            _context.Inbox.Remove(inbox);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InboxExists(string id)
        {
            return (_context.Inbox?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
