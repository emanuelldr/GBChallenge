using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GBChallenge.API.ViewModels
{
    public class AdicionarRevendedorRequest
    {
        [Required(ErrorMessage = "Campo Nome é Obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo CPF é Obrigatório")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Campo Email é Obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Senha é Obrigatório")]
        public string Senha { get; set; }
    }

    public class AutenticarRevendedorRequest
    {
        [Required(ErrorMessage = "Campo Login é Obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo Senha é Obrigatório")]
        public string Senha { get; set; }
    }
}
