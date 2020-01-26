using System;

namespace GBChallenge.Core.Domain.Entities
{
    public class Compra
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public double Valor { get; set; }
        public string Status { get; set; }
        public int IdRevendedor { get; set; }
        public Revendedor Revendedor { get; set; }
        public int PercentualCashBack { get; set; }
        public DateTime Data { get; set; }
    }
}
