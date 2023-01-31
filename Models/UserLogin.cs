using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.Models
{
    public class UserLogin : IdentityUserLogin<int>
    {
        [Key]
        public int IdLogin { get; set; }
    }
}
