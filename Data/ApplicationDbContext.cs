using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SC.AplicativoSenhorCaixa.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SC.AplicativoSenhorCaixa.Data
{
    public class ApplicationDbContext : IdentityDbContext<Application_User, Role, int, IdentityUserClaim<int>, UserRole, UserLogin,
                                                 IdentityRoleClaim<int>, UserToken>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<UsuarioLoja> UsuarioLoja { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


    }
}
