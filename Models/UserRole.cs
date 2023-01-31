using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.Models
{
    public class UserRole : IdentityUserRole<int>
    {
        public Application_User User { get; set; }
        public Role Role { get; set; }
    }
}
