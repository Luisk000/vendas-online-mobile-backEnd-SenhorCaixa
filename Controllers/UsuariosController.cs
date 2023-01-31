using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SC.AplicativoSenhorCaixa.Models;
using SC.AplicativoSenhorCaixa.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using SC.AplicativoSenhorCaixa.ViewModel;

namespace SC.AplicativoSenhorCaixa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly FirebirdContext _contexto;
        private readonly UserManager<Application_User> _userManager;
        private readonly SignInManager<Application_User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UsuariosController(UserManager<Application_User> userManager,
            SignInManager<Application_User> signInManager,
            FirebirdContext context,
            ApplicationDbContext contexto,
            RoleManager<Role> roleManager,
            IConfiguration configuration,
            IMapper mapper)
        {
            _contexto = context;
            _context = contexto;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task <IActionResult> Get()
        {
            var result = await _context.Users.ToListAsync();

            return Ok(result);
        }

        [HttpPost("ChangePassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword([FromBody] Application_UserViewModel userInfo)
        {
            try
            {
                Application_User user = await _userManager.FindByEmailAsync(userInfo.email);
                var result = await _userManager.ChangePasswordAsync(user, userInfo.oldPassword, userInfo.newPassword);
                return Ok(result.Succeeded);
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }
        }

        [HttpPost("ChangeEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangeEmail([FromBody] Application_UserViewModel userInfo)
        {
            try
            {
                Application_User user = await _userManager.FindByEmailAsync(userInfo.email);
                string token = await _userManager.GenerateChangeEmailTokenAsync(user, userInfo.newEmail);
                var result = await _userManager.ChangeEmailAsync(user, userInfo.newEmail, token);
                return Ok(result.Succeeded);
            }
            catch
            {
                return Ok(false);
            }
        }

        [HttpPost("ChangeLojaUsuario")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangeLojaUsuario([FromBody] Application_UserViewModel model)
        {
            try
            {
                Application_User user = _context.Users
                    .Where(u => u.Cpf == model.Cpf).SingleOrDefault();

                UsuarioLoja usuarioLoja = await _context.UsuarioLoja
                    .Where(u => u.idUsuario == user.Id.ToString() && u.IdCliente == model.idClienteSelecionado)
                    .SingleOrDefaultAsync();

                if (usuarioLoja == null)
                {
                    UsuarioLoja usuarioLojaNovo = new UsuarioLoja();
                    usuarioLojaNovo.IdCliente = (int)model.idClienteSelecionado;
                    usuarioLojaNovo.idUsuario = user.Id.ToString();
                    await _context.UsuarioLoja.AddAsync(usuarioLojaNovo);
                }
                else
                    _context.UsuarioLoja.Remove(usuarioLoja);

                await _context.SaveChangesAsync();
                return Json("Sucesso");
            }
            catch (Exception ex)
            {
                return Json("Erro: " + ex);
            }
        }

        [HttpPost("DeleteUser")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteUser([FromBody] Application_User userInfo)
        {
            try
            {
                Application_User user = await _userManager.FindByEmailAsync(userInfo.Email);
                var result = await _userManager.DeleteAsync(user);
                return Ok();
            }
            catch
            {
                return Ok(false);
            }
        }

        [HttpPost("Criar")]
        [AllowAnonymous]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] Application_UserViewModel model)
        {
            try
            {
                var user = new Application_User
                {
                    UserName = model.userName,
                    Email = model.email,
                    Cpf = model.Cpf,
                    RedeLoja = model.RedeLoja,
                    Ativo = true
                };               

                if (user.ValidaCpf(model.Cpf) == false)
                    return BadRequest("CPF inválido");

                var result = await _userManager.CreateAsync(user, model.newPassword);
                await _userManager.AddToRoleAsync(user, model.roleName);

                if (result.Succeeded)
                {
                    await AddClientes(model);
                    return BuildToken(user);
                }
                else if (UsuarioExiste(model.Cpf))
                {
                    await AddClientes(model);
                    return StatusCode(500, "Usuário já existe");
                }
                else
                    return StatusCode(500, "Login Inválido");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Login Inválido");
            }           
        }
       
        public async Task AddClientes(Application_UserViewModel model)
        {
            Application_User user = _context.Users
                .Where(u => u.UserName == model.userName).SingleOrDefault();

            if (model.clientes != null)
            {
                foreach (Cliente cliente in model.clientes)
                {
                    UsuarioLoja usuarioLoja = new UsuarioLoja();
                    usuarioLoja.IdCliente = cliente.CD_CLIENTE;
                    usuarioLoja.idUsuario = user.Id.ToString();
                    await _context.UsuarioLoja.AddAsync(usuarioLoja);
                }
                await _context.SaveChangesAsync();
            }          
        }


        [HttpPost("VerificarRole")]
        [AllowAnonymous]
        public IActionResult VerificarRole(Application_UserViewModel model)
        {
            Application_User user = _context.Users
                .Where(u => u.UserName == model.userName).SingleOrDefault();         

            UserRole userRole = _context.UserRoles
                .Where(ur => ur.UserId == user.Id).SingleOrDefault();

            Role role = _context.Roles
               .Where(r => r.Id == userRole.RoleId).SingleOrDefault();

            return Json(role.Name);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Application_User userInfo)
        {
            try
            {
                Application_User user = await _userManager.FindByNameAsync(userInfo.UserName);

                if (user.Ativo == true)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, userInfo.PasswordHash, false);

                    //var roles = await _userManager.GetRolesAsync(user);////

                    if (result.Succeeded)
                    {
                        Application_User appUser = await _userManager.Users
                            .FirstOrDefaultAsync(u => u.NormalizedUserName == userInfo.UserName.ToUpper());

                        var userToReturn = _mapper.Map<Application_User>(appUser);
                        return Ok(new
                        {
                            token = GenerateToken(appUser).Result,
                            user = userToReturn,
                        });
                    }
                }          

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou {ex.Message}");
            }
            //var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.PasswordHash,
            //    isPersistent: false, lockoutOnFailure: false);

            //if (result.Succeeded)
            //{
            //    return BuildToken(userInfo);
            //}
            //else
            //{
            //    ModelState.AddModelError(string.Empty, "Login inválido.");
            //    return BadRequest(ModelState);
            //}
        }

        public int GetNewUserId()
        {
            List<Application_User> users = _context.Users.ToList();
            int id = users.Count + 1;
            return id;
        }

        private UserToken BuildToken(Application_User userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }


        private async Task<string> GenerateToken(Application_User userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            //JwtSecurityToken token = new JwtSecurityToken(
            //    issuer: null,
            //    audience: null,
            //    claims: claims,
            //    expires: expiration,
            //    signingCredentials: creds);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(9.0),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private bool UsuarioExiste(string cpf)
        {
            return _context.Users.Any(e => e.Cpf == cpf);
        }

        [HttpGet]
        [Route("[action]/{userCpf}")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangeUser(string userCpf)
        {
            try
            {
                Application_User user = await _context.Users
                    .Where(u => u.Cpf == userCpf).FirstOrDefaultAsync();

                if (user.Ativo == true)
                    user.Ativo = false;
                else
                    user.Ativo = true;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return Json("Sucesso");
            }
            catch (Exception ex)
            {
                return Json("Erro: " + ex.ToString());
            }
        }

        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllUsers([FromBody] Application_User userInfo)
        {
            try
            {
                Application_User[] users = await _context.Users
                    .Where(u => u.RedeLoja == userInfo.RedeLoja && u.Cpf != userInfo.Cpf)
                    .ToArrayAsync();

                return Json(users);
            }
            catch (Exception ex)
            {
                return Json("Erro: " + ex.ToString());
            }
        }
    }
}
