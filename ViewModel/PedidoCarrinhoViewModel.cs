using SC.AplicativoSenhorCaixa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.ViewModel
{
    public class PedidoCarrinhoViewModel
    {
        public int CD_CLIENTE { get; set; }
        public string NM_FANTASIA { get; set; }
        public Carrinho[] produtosCarrinho { get; set; }
    }
}
