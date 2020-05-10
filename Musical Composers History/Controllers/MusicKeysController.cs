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
    public class MusicKeysController : ControllerBase
    {
        private readonly ComposersDbContext _context;

        public MusicKeysController(ComposersDbContext context)
        {
            _context = context;
        }

        // GET: api/MusicKeys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MusicKey>>> GetMusicKeys()
        {
            return await _context.MusicKeys.ToListAsync();
        }

        // GET: api/MusicKeys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MusicKey>> GetMusicKey(int id)
        {
            var musicKey = await _context.MusicKeys.FindAsync(id);

            if (musicKey == null)
            {
                return NotFound();
            }

            return musicKey;
        }

        // PUT: api/MusicKeys/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMusicKey(int id, MusicKey musicKey)
        {
            if (id != musicKey.Id)
            {
                return BadRequest();
            }

            _context.Entry(musicKey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusicKeyExists(id))
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

        // POST: api/MusicKeys
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MusicKey>> PostMusicKey(MusicKey musicKey)
        {
            _context.MusicKeys.Add(musicKey);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMusicKey", new { id = musicKey.Id }, musicKey);
        }

        // DELETE: api/MusicKeys/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MusicKey>> DeleteMusicKey(int id)
        {
            var musicKey = await _context.MusicKeys.FindAsync(id);
            if (musicKey == null)
            {
                return NotFound();
            }

            _context.MusicKeys.Remove(musicKey);
            await _context.SaveChangesAsync();

            return musicKey;
        }

        private bool MusicKeyExists(int id)
        {
            return _context.MusicKeys.Any(e => e.Id == id);
        }
    }
}
