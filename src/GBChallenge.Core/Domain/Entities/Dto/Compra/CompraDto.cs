
using System;

namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class CompraDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public double Valor { get; set; }
        public int PercentualCashBack { get; set; }
        public DateTime Data { get; set; }
    }
}
