using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueBox.Contexts;
using QueBox.Models;

namespace MiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisenoEFController : ControllerBase
    {
        private readonly ApiContext _context;

        public DisenoEFController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/DisenoEF
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diseno>>> GetDisenos()
        {
            return await _context.Disenos.ToListAsync();
        }

        // GET: api/DisenoEF/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Diseno>> GetDiseno(int id)
        {
            var diseno = await _context.Disenos.FindAsync(id);

            if (diseno == null)
            {
                return NotFound();
            }

            return diseno;
        }

        // PUT: api/DisenoEF/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiseno(int id, Diseno diseno)
        {
            if (id != diseno.Id_Diseno)
            {
                return BadRequest();
            }

            _context.Entry(diseno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DisenoExists(id))
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

        // POST: api/DisenoEF
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Diseno>> PostDiseno(Diseno diseno)
        {
            _context.Disenos.Add(diseno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiseno", new { id = diseno.Id_Diseno }, diseno);
        }

        // DELETE: api/DisenoEF/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiseno(int id)
        {
            var diseno = await _context.Disenos.FindAsync(id);
            if (diseno == null)
            {
                return NotFound();
            }

            _context.Disenos.Remove(diseno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DisenoExists(int id)
        {
            return _context.Disenos.Any(e => e.Id_Diseno == id);
        }
    }
}
