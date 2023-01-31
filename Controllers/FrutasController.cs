using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SC.AplicativoSenhorCaixa.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class FrutasController : ControllerBase
    {
        private readonly FirebirdContext _context;

        public FrutasController(FirebirdContext contexto)
        {
            _context = contexto;
        }

        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _context.USUARIOCLIENTE.AsNoTracking().ToListAsync();

                return Ok(result);
            }
            catch
            {
                return Ok("Nao encontrado");
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<string> GetById (int id)
        {
            var result = id == 3;

            return Ok(result);
        }
    }
}
