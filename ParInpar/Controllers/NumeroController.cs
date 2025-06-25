using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParInpar.Models;

namespace ParInpar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NumeroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NumeroController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/numero
        [HttpGet]
        public IActionResult GetTodos()
        {
            var numeros = _context.Numeros.ToList();
            return Ok(numeros);
        }

        // GET: api/numero/verificar/8
        [HttpGet("verificar/{numero}")]
        public IActionResult Verificar(int numero)
        {
            var resultado = numero % 2 == 0 ? "Par" : "Impar";
            return Ok(new { numero, resultado });
        }

        // POST: api/numero/verificar
        [HttpPost("verificar")]
        public IActionResult VerificarPost([FromBody] NumeroVerificado numero)
        {
            var resultado = numero.Valor % 2 == 0 ? "Par" : "Impar";
            return Ok(new { numero.Valor, resultado });
        }

        // POST: api/numero
        [HttpPost]
        [Authorize]
        public IActionResult Guardar([FromBody] NumeroVerificado numero)
        {
            // POST
            numero.EsPar = numero.Valor % 2 == 0;

            _context.Numeros.Add(numero);
            _context.SaveChanges();
            return Ok(numero);
        }

        // PUT: api/numero/{id}
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Editar(int id, [FromBody] NumeroVerificado numeroEditado)
        {
            var numero = _context.Numeros.FirstOrDefault(n => n.Id == id);
            if (numero == null)
            {
                return NotFound(new { mensaje = "Número no encontrado" });
            }

            numero.Valor = numeroEditado.Valor;
            // PUT
            numero.EsPar = numeroEditado.Valor % 2 == 0;
     

            _context.SaveChanges();
            return Ok(numero);
        }

        // DELETE: api/numero/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Eliminar(int id)
        {
            var numero = _context.Numeros.FirstOrDefault(n => n.Id == id);
            if (numero == null)
            {
                return NotFound(new { mensaje = "Número no encontrado" });
            }

            _context.Numeros.Remove(numero);
            _context.SaveChanges();
            return Ok(new { mensaje = "Número eliminado correctamente" });
        }
    }
}

