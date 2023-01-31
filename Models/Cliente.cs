using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.Models
{
    public class Cliente
    {
        [Key]
        public int CD_CLIENTE { get; set; }
        public string CD_CNPJ { get; set; }
        public int CD_BAIRRO { get; set; }
        public int CD_REDELOJA { get; set; }
        public string NM_FANTASIA { get; set; }
        public int CD_MATRIZ { get; set; }
        public char ST_PEDIDOAPP { get; set; }
        
    }
}
