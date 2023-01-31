using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.Models
{
    public class CodPedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CD_PEDIDO { get; set; }
        public int CD_EMPRESA { get; set; } = 2;     
        public char SG_STATUS { get; set; } = 'A';
    }
}
