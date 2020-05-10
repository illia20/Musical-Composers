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
    public class PiecesController : ControllerBase
    {
        private readonly ComposersDbContext _context;

        public PiecesController(ComposersDbContext context)
        {
            _context = context;
        }

        // GET: api/Pieces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Piece>>> GetPieces()
        {
            return await _context.Pieces.ToListAsync();
        }

        // GET: api/Pieces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Piece>> GetPiece(int id)
        {
            var piece = await _context.Pieces.FindAsync(id);

            if (piece == null)
            {
                return NotFound();
            }

            return piece;
        }

        // PUT: api/Pieces/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPiece(int id, Piece piece)
        {
            if (id != piece.Id)
            {
                return BadRequest();
            }

            _context.Entry(piece).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PieceExists(id))
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

        // POST: api/Pieces
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Piece>> PostPiece(Piece piece)
        {
            _context.Pieces.Add(piece);
            /*
             * custom add genre to composer's genres
             */
            ComposerGenres composerGenres = new ComposerGenres();
            composerGenres.Genre = piece.Genre;
            composerGenres.Composer = piece.Composer;
            if (!_context.ComposerGenres.Any(g => g.Genre == composerGenres.Genre && g.Composer == composerGenres.Composer))
                _context.ComposerGenres.Add(composerGenres);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPiece", new { id = piece.Id }, piece);
        }

        // DELETE: api/Pieces/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Piece>> DeletePiece(int id)
        {
            var piece = await _context.Pieces.FindAsync(id);
            if (piece == null)
            {
                return NotFound();
            }
            // custom remove genre
            var genre = piece.Genre;
            var composer = piece.Composer;
            var cgs = composer.ComposerGenres.ToList();
            int count = 0;
            foreach(var cg in cgs)
            {
                if(cg.Genre == genre)
                {
                    count++;
                }
            }
            if(count == 1)
            {
                ComposerGenres g = _context.ComposerGenres.First(q => q.Genre == genre);
                _context.ComposerGenres.Remove(g);
            }

            _context.Pieces.Remove(piece);
            await _context.SaveChangesAsync();

            return piece;
        }

        private bool PieceExists(int id)
        {
            return _context.Pieces.Any(e => e.Id == id);
        }
    }
}
