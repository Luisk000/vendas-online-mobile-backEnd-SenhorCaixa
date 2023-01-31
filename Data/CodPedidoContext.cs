using Microsoft.EntityFrameworkCore;
using SC.AplicativoSenhorCaixa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.Data
{
    public class CodPedidoContext: DbContext
    {
        public CodPedidoContext(DbContextOptions<CodPedidoContext> options) : base(options)
        { }

        public DbSet<CodPedido> CODPEDIDO { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
