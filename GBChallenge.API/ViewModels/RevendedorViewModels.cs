using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBChallenge.API.ViewModels
{
    public class AdicionarRevendedorRequest
    {     
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class AutenticarRevendedorRequest
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
