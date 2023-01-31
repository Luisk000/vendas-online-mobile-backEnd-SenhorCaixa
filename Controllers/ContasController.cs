using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SC.AplicativoSenhorCaixa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.Controllers
{
    [Route("api/[controller]")]
    public class ContasController : Controller
    {
        private readonly UserManager<UsuaCliente> _userManager;
        private readonly SignInManager<UsuaCliente> _signInManager;
        
        public ContasController(
            UserManager<UsuaCliente> userManager,
            SignInManager<UsuaCliente> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UsuaCliente cliente, string returnUrl = null)
        {
            ViewData["ReurnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new UsuaCliente { NM_USUARIO = cliente.NM_USUARIO, NM_EMAIL = cliente.NM_EMAIL };
                var result = await _userManager.CreateAsync(user, cliente.NM_SENHA);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(result);
                }
                return Ok(result);
            }
            return Json("");
        }
        
    }
}
