using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.Models
{
    public class Produto
    {
        [Key]
        public int CD_PRODUTO { get; set; }
        public string NM_PRODUTO { get; set; }
        public string QT_QTDENAEMBALAGEM { get; set; }

    }
}
