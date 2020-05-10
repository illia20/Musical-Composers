using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Musical_Composers_History.Models;

namespace Musical_Composers_History.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComposersController : ControllerBase
    {
        private readonly ComposersDbContext _context;

        public ComposersController(ComposersDbContext context)
        {
            _context = context;
        }

        // GET: api/Composers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Composer>>> GetComposers()
        {
            return await _context.Composers.ToListAsync();
        }

        // GET: api/Composers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Composer>> GetComposer(int id)
        {
            var composer = await _context.Composers.FindAsync(id);

            if (composer == null)
            {
                return NotFound();
            }

            return composer;
        }

        // PUT: api/Composers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComposer(int id, Composer composer)
        {
            if (id != composer.Id)
            {
                return BadRequest();
            }

            _context.Entry(composer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComposerExists(id))
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

        // POST: api/Composers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Composer>> PostComposer(Composer composer)
        {
            _context.Composers.Add(composer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComposer", new { id = composer.Id }, composer);
        }

        // DELETE: api/Composers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Composer>> DeleteComposer(int id)
        {
            var composer = await _context.Composers.FindAsync(id);
            if (composer == null)
            {
                return NotFound();
            }

            _context.Composers.Remove(composer);
            await _context.SaveChangesAsync();

            return composer;
        }

        private bool ComposerExists(int id)
        {
            return _context.Composers.Any(e => e.Id == id);
        }
    }
}
