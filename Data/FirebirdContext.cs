using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SC.AplicativoSenhorCaixa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.Data
{
    public class FirebirdContext : DbContext
    {
        public FirebirdContext(DbContextOptions<FirebirdContext> options) :base(options)
        {

        }
        public DbSet<Cliente> CLIENTE { get; set; }
        public DbSet<TabelaPrecoCliente> TABELAPRECOCLIENTE { get; set; }
        public DbSet<Produto> PRODUTO { get; set; }
        public DbSet<PedidoVendaCab> PEDIDOVENDACAB { get; set; }
        public DbSet<PedidoVendaDet> PEDIDOVENDADET { get; set; }
        public DbSet<Carrinho> CARRINHO { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });

            builder.Entity<PedidoVendaDet>().HasKey(pedido => new {
                pedido.CD_PEDIDO,
                pedido.CD_ITEM,
                pedido.CD_EMPRESA
            });
        }
    }
}
