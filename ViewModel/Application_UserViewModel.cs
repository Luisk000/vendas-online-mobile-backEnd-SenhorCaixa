using Microsoft.AspNetCore.Identity;
using SC.AplicativoSenhorCaixa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.AplicativoSenhorCaixa.ViewModel
{
    public class Application_UserViewModel
    {
        public int? id { get; set; }
        public string Cpf { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public int RedeLoja { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string newEmail { get; set; }
        public string roleName { get; set; }
        public Cliente[] clientes { get; set; }
        public int? idClienteSelecionado { get; set; }
    }
}
