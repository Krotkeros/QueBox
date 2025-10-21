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
    public class ImagenDecorativaEFController : ControllerBase
    {
        private readonly ApiContext _context;

        public ImagenDecorativaEFController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/ImagenDecorativaEF
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagenDecorativa>>> GetImagenDecorativas()
        {
            return await _context.ImagenDecorativas.ToListAsync();
        }

        // GET: api/ImagenDecorativaEF/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImagenDecorativa>> GetImagenDecorativa(int id)
        {
            var imagenDecorativa = await _context.ImagenDecorativas.FindAsync(id);

            if (imagenDecorativa == null)
            {
                return NotFound();
            }

            return imagenDecorativa;
        }

        // PUT: api/ImagenDecorativaEF/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImagenDecorativa(int id, ImagenDecorativa imagenDecorativa)
        {
            if (id != imagenDecorativa.id_img)
            {
                return BadRequest();
            }

            _context.Entry(imagenDecorativa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagenDecorativaExists(id))
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

        // POST: api/ImagenDecorativaEF
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImagenDecorativa>> PostImagenDecorativa(ImagenDecorativa imagenDecorativa)
        {
            _context.ImagenDecorativas.Add(imagenDecorativa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImagenDecorativa", new { id = imagenDecorativa.id_img }, imagenDecorativa);
        }

        // DELETE: api/ImagenDecorativaEF/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImagenDecorativa(int id)
        {
            var imagenDecorativa = await _context.ImagenDecorativas.FindAsync(id);
            if (imagenDecorativa == null)
            {
                return NotFound();
            }

            _context.ImagenDecorativas.Remove(imagenDecorativa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImagenDecorativaExists(int id)
        {
            return _context.ImagenDecorativas.Any(e => e.id_img == id);
        }
    }
}
