using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.Models
{
    public class UserToken : IdentityUserToken<int>
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
