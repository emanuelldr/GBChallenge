using System;

namespace GBChallenge.Domain.Entities
{
    public class Compra
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public double Valor { get; set; }
        public int IdRevendedor { get; set; }
        public Revendedor Revendedor { get; set; }
        public DateTime Data { get; set; }
    }
}
