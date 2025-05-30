using Microsoft.AspNetCore.Mvc;

namespace ParInpar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NumeroController : ControllerBase
    {
        [HttpGet("verificar/{numero}")]
        public IActionResult VerificarParImpar(int numero)
        {
            if (numero % 2 == 0)
            {
                return Ok($"El número {numero} es PAR.");
            }
            else
            {
                return Ok($"El número {numero} es IMPAR.");
            }
        }
    }
}
