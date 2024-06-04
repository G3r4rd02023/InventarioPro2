using Inventario.Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperacionesController : ControllerBase
    {
        private readonly DataContext _context;

        public OperacionesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Operaciones.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Operacion operacion)
        {
            _context.Add(operacion);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
