using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.Models
{
    public class Carrinho
    {
        [Key]
        public int CD_CARRINHO { get; set; }
        public int CD_PRODUTO { get; set; }
        public int? CD_PEDIDO { get; set; }
        public int CD_CLIENTE { get; set; }
        public string NM_FANTASIA { get; set; }
        public string NM_PRODUTO { get; set; }
        public string NM_CPFUSUARIO { get; set; }
        public int QT_PRODUTO { get; set; }
    }
}
