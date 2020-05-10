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
    public class ComposerGenresController : ControllerBase
    {
        private readonly ComposersDbContext _context;

        public ComposerGenresController(ComposersDbContext context)
        {
            _context = context;
        }

        // GET: api/ComposerGenres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComposerGenres>>> GetComposerGenres()
        {
            return await _context.ComposerGenres.ToListAsync();
        }

        // GET: api/ComposerGenres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComposerGenres>> GetComposerGenres(int id)
        {
            var composerGenres = await _context.ComposerGenres.FindAsync(id);

            if (composerGenres == null)
            {
                return NotFound();
            }

            return composerGenres;
        }

        // PUT: api/ComposerGenres/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComposerGenres(int id, ComposerGenres composerGenres)
        {
            if (id != composerGenres.Id)
            {
                return BadRequest();
            }

            _context.Entry(composerGenres).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComposerGenresExists(id))
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

        // POST: api/ComposerGenres
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ComposerGenres>> PostComposerGenres(ComposerGenres composerGenres)
        {
            _context.ComposerGenres.Add(composerGenres);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComposerGenres", new { id = composerGenres.Id }, composerGenres);
        }

        // DELETE: api/ComposerGenres/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ComposerGenres>> DeleteComposerGenres(int id)
        {
            var composerGenres = await _context.ComposerGenres.FindAsync(id);
            if (composerGenres == null)
            {
                return NotFound();
            }
            /*
             * custom delete pieces by composer in genre
             */
            Composer c = composerGenres.Composer;
            Genre g = composerGenres.Genre;

            var p = _context.Pieces;
            foreach(var p1 in p)
            {
                if (p1.Composer == c && p1.Genre == g)
                    _context.Pieces.Remove(p1);
            }
            
            //

            _context.ComposerGenres.Remove(composerGenres);
            await _context.SaveChangesAsync();

            return composerGenres;
        }

        private bool ComposerGenresExists(int id)
        {
            return _context.ComposerGenres.Any(e => e.Id == id);
        }
    }
}
