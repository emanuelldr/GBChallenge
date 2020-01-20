using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBChallenge.API
{
    public class GBChallengeSettings
    {
        public string ChaveAPI { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }
        public int ExpiracaoMinutos { get; set; }
    }
}
