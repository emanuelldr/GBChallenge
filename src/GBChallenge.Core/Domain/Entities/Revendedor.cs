﻿using System.Collections.Generic;

namespace GBChallenge.Core.Domain.Entities
{
    public class Revendedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public List<Compra> Compras { get; set; }
    }
}
