using Microsoft.AspNetCore.Mvc;
using SC.AplicativoSenhorCaixa.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.Controllers
{

    
    public class LojasController : Controller
    {
        private readonly FirebirdContext _context;

        public LojasController(FirebirdContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(int id, int matriz)
        {
            var result = _context.CLIENTE.Where(a =>  a.ST_PEDIDOAPP == 'S').ToList();

            return Json(result);
        }
    }
}
