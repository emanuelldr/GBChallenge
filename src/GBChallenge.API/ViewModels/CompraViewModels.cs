using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GBChallenge.API.ViewModels
{
    public class AdicionarCompraRequest
    {
        [Required(ErrorMessage = "Campo Codigo é Obrigatório")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Campo Valor é Obrigatório")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "Campo Data é Obrigatório")]
        public string Data { get; set; }

        [Required(ErrorMessage = "Campo Senha é Obrigatório")]
        public string CPFRevendedor { get; set; }
    }

    public class EditarCompraRequest
    {
        public string Codigo { get; set; }

        public double Valor { get; set; }

        public string Data { get; set; }

        public string CPFRevendedor { get; set; }
    }
}
