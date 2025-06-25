using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParInpar.Models;
using System.Linq;

namespace ParInpar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CifradoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CifradoController(AppDbContext context)
        {
            _context = context;
        }

        // POST: /api/cifrado/encriptar
        [HttpPost("encriptar")]
        public IActionResult EncriptarTextoPost([FromBody] TextoRequest request)
        {
            var resultado = CifrarCesar(request.Texto);
            return Ok(resultado);
        }

        // POST: /api/cifrado/desencriptar
        [HttpPost("descencriptar")]
        public IActionResult DesencriptarTextoPost([FromBody] TextoRequest request)
        {
            var resultado = DescifrarCesar(request.Texto);
            return Ok(resultado);
        }

        // GET: /api/cifrado
        [HttpGet]
        public IActionResult GetTodos()
        {
            var datos = _context.Cifrados.ToList();
            return Ok(datos);
        }

        // POST: /api/cifrado
        [Authorize]
        [HttpPost]
        public IActionResult Guardar([FromBody] TextoCifrado nuevo)
        {
            _context.Cifrados.Add(nuevo);
            _context.SaveChanges();
            return Ok(nuevo);
        }

        // PUT: /api/cifrado/{id}
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] TextoCifrado actualizado)
        {
            var existente = _context.Cifrados.Find(id);
            if (existente == null) return NotFound();

            existente.TextoOriginal = actualizado.TextoOriginal;
            existente.TextoCifradoValor = actualizado.TextoCifradoValor;
                                                                                  // 🔥 CAMBIO AQUÍ
            existente.Desplazamiento = actualizado.Desplazamiento;
            existente.FechaRegistro = actualizado.FechaRegistro;

            _context.SaveChanges();

            return Ok(existente);
        }

        // DELETE: /api/cifrado/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            var encontrado = _context.Cifrados.Find(id);
            if (encontrado == null) return NotFound();

            _context.Cifrados.Remove(encontrado);
            _context.SaveChanges();

            return NoContent();
        }

        // Lógica del cifrado y descifrado tipo César
        private string CifrarCesar(string texto)
        {
            return new string(texto.Select(c => (char)(c + 3)).ToArray());
        }

        private string DescifrarCesar(string texto)
        {
            return new string(texto.Select(c => (char)(c - 3)).ToArray());
        }
    }

    public class TextoRequest
    {
        public string Texto { get; set; } = string.Empty;
    }
}




