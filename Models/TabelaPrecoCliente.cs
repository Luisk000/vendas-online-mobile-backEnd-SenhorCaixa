using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.Models
{
    public class TabelaPrecoCliente
    {
        [Key]
        public int CD_CLIENTE { get; set; }
        public int CD_PRODUTO { get; set; }
        public string NM_PRODUTO_ANT { get; set; }
        public double VL_UNIATRIO { get; set; }
    }
}
