using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.Models
{
    public class PedidoVendaDet
    {
        [Key]        
        public int CD_PEDIDO { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CD_ITEM { get; set; }
        public int? QT_PEDIDA { get; set; }
        public int CD_PRODUTO { get; set; }
        public decimal VL_UNITARIO { get; set; } = 0;
        public decimal VL_IPIFAT { get; set; } = 0;
        public decimal VL_FRETE { get; set; } = 0;
        public decimal VL_DESCONTO { get; set; } = 0;
        public decimal PC_DESCONTO { get; set; } = 0;
        public decimal VL_SELO { get; set; } = 0;
        [Key]
        public decimal CD_EMPRESA { get; set; } = 2;
        public decimal PC_COMISSAOPRODUTO { get; set; } = 0;
        public decimal PC_CONDESPECIAL { get; set; } = 0;
        public decimal VL_CONDESPECIAL { get; set; } = 0;
        public decimal VL_TOTALITEMFAT { get; set; } = 0;
        public decimal VL_TOTALITEMVDA { get; set; } = 0;
        public decimal VL_TOTALITEMFATLIQ { get; set; } = 0;
        public decimal VL_TOTALITEMVDALIQ { get; set; } = 0;
        public decimal VL_UNITARIOTOTAL { get; set; } = 0;
        public decimal PC_IPI { get; set; } = 0;
        public decimal VL_UNITARIONF { get; set; } = 0;
        public decimal VL_IPIVDA { get; set; } = 0;
        public decimal VL_IPIUNIT { get; set; } = 0;
        public decimal VL_IPI { get; set; } = 0;
        public decimal VL_UNITARIOFINAL { get; set; } = 0;
        public decimal VL_TOTALITEMFINAL { get; set; } = 0;
        public decimal VL_UNITARIOLIQ { get; set; } = 0;
        public decimal VL_TOTALITEMFINALFAT { get; set; } = 0;
        public decimal VL_TOTALITEMFINALVDA { get; set; } = 0;
    }
}
