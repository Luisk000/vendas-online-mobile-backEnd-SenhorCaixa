using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SC.AplicativoSenhorCaixa.Data;
using SC.AplicativoSenhorCaixa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class LojaController : Controller
    {
        private readonly FirebirdContext _context;
        private readonly ApplicationDbContext _contexto;

        public LojaController(FirebirdContext context, ApplicationDbContext contexto)
        {
            _context = context;
            _contexto = contexto;
        }
        [HttpPost("[action]")]
        public async Task<ActionResult> GetLojas([FromBody] Application_User user)
        {
            try
            {
                Application_User userDb = await _contexto.Users
                    .Where(u => u.Cpf == user.Cpf).FirstOrDefaultAsync();

                UserRole userRole = await _contexto.UserRoles
                    .Where(ur => ur.UserId == userDb.Id).FirstOrDefaultAsync();

                Role role = await _contexto.Roles
                    .Where(r => r.Id == userRole.RoleId).FirstOrDefaultAsync();

                List<Cliente> clientes = new List<Cliente>();

                if (role.Name == "admin" || role.Name == "master")
                    clientes = await _context.CLIENTE.Where(a => a.CD_REDELOJA == user.RedeLoja).ToListAsync();

                else
                {
                    List<UsuarioLoja> userLojas = await _contexto.UsuarioLoja
                        .Where(ul => ul.idUsuario == userDb.Id.ToString()).ToListAsync();
                    
                    foreach(UsuarioLoja userLoja in userLojas)
                    {
                        Cliente cliente = await _context.CLIENTE
                            .Where(c => c.CD_CLIENTE == userLoja.IdCliente).FirstOrDefaultAsync();

                        clientes.Add(cliente);
                    }                      
                }

                return Json(clientes);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetLojasUsuario/{cpf}")]
        public async Task<ActionResult> GetLojasUsuario(string cpf)
        {
            try
            {
                Application_User user = await _contexto.Users
                    .Where(u => u.Cpf == cpf).FirstOrDefaultAsync();

                List<Cliente> clientes = await _context.CLIENTE
                    .Where(a => a.CD_REDELOJA == user.RedeLoja).ToListAsync();

                List<int> userLojasIds = await _contexto.UsuarioLoja
                    .Where(u => u.idUsuario == user.Id.ToString())
                    .Select(u => u.IdCliente).ToListAsync();

                return Json(userLojasIds);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
