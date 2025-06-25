using Microsoft.AspNetCore.Mvc;
using ParInpar.Models;

namespace ParInpar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] Usuario usuario)
        {
            // Evitar duplicados
            if (_context.Usuarios.Any(u => u.Correo == usuario.Correo))
            {
                return BadRequest("El correo ya está registrado.");
            }

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return Ok("Usuario registrado correctamente.");
        }
    }
}
