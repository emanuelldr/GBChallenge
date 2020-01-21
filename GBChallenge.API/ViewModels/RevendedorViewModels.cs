using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GBChallenge.API.ViewModels
{
    public class AdicionarRevendedorRequest
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }

    public class AutenticarRevendedorRequest
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
