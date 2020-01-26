using System;
using System.Collections.Generic;
using System.Text;

namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class RevendedorDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public bool CompraAutoAprovada { get; set; }
    }
}
