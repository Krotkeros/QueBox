using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueBox.Contexts;
using QueBox.Models;

namespace QueBox.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapaEFController : ControllerBase
    {
        private readonly ApiContext _context;

        public CapaEFController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/CapaEF
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Capa>>> GetCapas()
        {
            return await _context.Capas.ToListAsync();
        }

        // GET: api/CapaEF/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Capa>> GetCapas(int id)
        {
            var capa = await _context.Capas.FindAsync(id);

            if (capa == null)
            {
                return NotFound();
            }

            return capa;
        }

        // PUT: api/CapaEF/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCapas(int id, Capa capa)
        {
            if (id != capa.Id)
            {
                return BadRequest();
            }

            _context.Entry(capa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CapaExists(id))
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

        // POST: api/CapaEF
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Capa>> PostCapa(Capa capa)
        {
            _context.Capas.Add(capa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCapa", new { id = capa.Id }, capa);
        }

        // DELETE: api/CapaEF/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCapa(int id)
        {
            var capa = await _context.Capas.FindAsync(id);
            if (capa == null)
            {
                return NotFound();
            }

            _context.Capas.Remove(capa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CapaExists(int id)
        {
            return _context.Capas.Any(e => e.Id == id);
        }
    }
}
