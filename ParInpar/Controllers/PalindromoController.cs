using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParInpar.Models;

namespace ParInpar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PalindromoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PalindromoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("verificar/{palabra}")]
        public IActionResult VerificarPalabraUrl(string palabra)
        {
            bool esPalindromo = EsPalindromo(palabra);
            return Ok(new { palabra, esPalindromo });
        }

        [HttpPost("verificar")]
        public IActionResult VerificarPalabraJson([FromBody] PalabraRequest request)
        {
            bool esPalindromo = EsPalindromo(request.Palabra);
            return Ok(new { palabra = request.Palabra, esPalindromo });
        }

        [HttpGet]
        public IActionResult ObtenerPalabras()
        {
            var palabras = _context.PalabrasVerificadas.ToList();
            return Ok(palabras);
        }

        [HttpPost]
        [Authorize]
        public IActionResult GuardarPalabra([FromBody] PalabraRequest request)
        {
            bool esPalindromo = EsPalindromo(request.Palabra);

            var nueva = new PalabraVerificada
            {
                Palabra = request.Palabra,
                EsPalindromo = esPalindromo
            };

            _context.PalabrasVerificadas.Add(nueva);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObtenerPalabras), new { id = nueva.Id }, nueva);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult ActualizarPalabra(int id, [FromBody] PalabraRequest request)
        {
            var palabraExistente = _context.PalabrasVerificadas.FirstOrDefault(p => p.Id == id);
            if (palabraExistente == null)
                return NotFound(new { mensaje = "Palabra no encontrada" });

            palabraExistente.Palabra = request.Palabra;
            palabraExistente.EsPalindromo = EsPalindromo(request.Palabra);

            _context.SaveChanges();
            return Ok(palabraExistente);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult EliminarPalabra(int id)
        {
            var palabra = _context.PalabrasVerificadas.FirstOrDefault(p => p.Id == id);
            if (palabra == null)
                return NotFound(new { mensaje = "No existe el registro" });

            _context.PalabrasVerificadas.Remove(palabra);
            _context.SaveChanges();

            return Ok(new { mensaje = "Eliminado correctamente" });
        }

        private bool EsPalindromo(string palabra)
        {
            string limpia = new string(palabra.ToLower().Where(char.IsLetterOrDigit).ToArray());
            return limpia.SequenceEqual(limpia.Reverse());
        }
    }

    public class PalabraRequest
    {
        public string Palabra { get; set; } = string.Empty;
    }
}
