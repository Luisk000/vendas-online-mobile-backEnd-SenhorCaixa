using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.Models
{
    public class PedidoVendaCab
    {
        [Key]
        public int CD_PEDIDO { get; set; }
        public int CD_CLIENTE { get; set; }
        public int CD_EMPRESA { get; set; } = 2;
        public decimal PC_REAJUSTE { get; set; } = 0;
        public decimal PC_COMISSAOREPRESENTANTE { get; set; } = 0;
        public decimal VL_PEDIDO { get; set; } = 0;
        public decimal VL_BASECOMISSAO { get; set; } = 0;
        public decimal PC_DESCONTO { get; set; } = 0;
        public decimal VL_DESCONTO { get; set; } = 0;
        public decimal PC_COMISSAOEMPRESA { get; set; } = 0;
        public decimal VL_PEDIDOSALDO { get; set; } = 0;
        public decimal VL_DEPOSITO { get; set; } = 0;
        public decimal PC_ATENCIPACAO { get; set; } = 0;
        public decimal VL_ABATIMENTO { get; set; } = 0;
        public decimal VL_IMPOSTOS { get; set; } = 0;
        public decimal VL_FRETE { get; set; } = 0;
        public decimal VL_BASECOMISSAOPED { get; set; } = 0;
        public decimal VL_BASECOMISSAOFAT { get; set; } = 0;
        public decimal VL_FRETEPED { get; set; } = 0;
        public decimal VL_FRETEFAT { get; set; } = 0;
        public decimal VL_IMPOSTOSPED { get; set; } = 0;
        public decimal VL_IMPOSTOSFAT { get; set; } = 0;
        public decimal VL_FATURADO { get; set; } = 0;
        public decimal VL_SEMCOMISSAOPED { get; set; } = 0;
        public decimal VL_SEMCOMISSAOFAT { get; set; } = 0;
        public decimal VL_DESCONTOPED { get; set; } = 0;
        public decimal VL_DESCONTOFAT { get; set; } = 0;
        public decimal VL_PEDIDOLIQUIDO { get; set; } = 0;
        public decimal VL_FATURADOLIQUIDO { get; set; } = 0;
        public decimal VL_FATURADOSALDO { get; set; } = 0;
        public decimal VL_FRETEREAL { get; set; } = 0;     
        public decimal VL_IPIPED { get; set; } = 0;
        public decimal VL_IPIFAT { get; set; } = 0;
        public decimal VL_NOTAFISCAL { get; set; } = 0;
        public decimal VL_NOTAFISCALFAT { get; set; } = 0;
        public decimal VL_NOTAFISCALPED { get; set; } = 0;
        public string ST_USARFRETEREAL { get; set; } = "N";
        public string ST_BAIXAENTREGA { get; set; } = "N";
        public string ST_APLICARDESCONTO { get; set; } = "N";
        public string ST_PRIMEIROPEDIDO { get; set; } = "N";
        public DateTime DT_PEDIDO { get; set; } = DateTime.Now.Date;
        public DateTime? DT_ENTREGA { get; set; }
        public int CD_ORIGEM { get; set; } = 2;
    }
}
