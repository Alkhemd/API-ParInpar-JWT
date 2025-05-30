using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ParInpar.Controllers // Asegúrate de que el namespace coincida
{
    [ApiController]
    [Route("api/[controller]")]
    public class PalindromoController : ControllerBase
    {
        [HttpGet("verificar/{palabra}")]
        public IActionResult VerificarPalindromo(string palabra)
        {
            if (string.IsNullOrWhiteSpace(palabra))
            {
                return BadRequest("La palabra no puede estar vacía.");
            }
            var palabraNormalizada = new string(palabra.Where(char.IsLetterOrDigit).ToArray()).ToLower();
            var palabraReversa = new string(palabraNormalizada.Reverse().ToArray());
            bool esPalindromo = palabraNormalizada == palabraReversa;
            if (esPalindromo)
            {
                return Ok($"La palabra '{palabra}' SÍ es un palíndromo.");
            }
            else
            {
                return Ok($"La palabra '{palabra}' NO es un palíndromo.");
            }
        }
    }
}