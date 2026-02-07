using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Peminjaman.Backend.Data;
using Peminjaman.Backend.Models;

namespace Peminjaman.Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PeminjamanController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PeminjamanController(AppDbContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeminjamanRuangan>>> GetAll()
        {
            return await _context.Peminjamans.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PeminjamanRuangan>> Create(PeminjamanRuangan input)
        {

            _context.Peminjamans.Add(input);

            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetAll), new { id = input.Id }, input);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PeminjamanRuangan input)
        {
            if (id != input.Id) return BadRequest();
            _context.Entry(input).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Peminjamans.Any(e => e.Id == id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.Peminjamans.FindAsync(id);

            if (data == null) return NotFound();

            _context.Peminjamans.Remove(data);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPeminjamanById(int id)
        {
            var data = await _context.Peminjamans.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }
    }
}