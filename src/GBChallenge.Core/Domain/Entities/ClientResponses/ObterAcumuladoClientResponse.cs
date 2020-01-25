using System;
using System.Collections.Generic;
using System.Text;

namespace GBChallenge.Core.Domain.Entities.ClientResponses
{
    public class ObterAcumuladoClientResponse
    {
        public ResponseBody Body { get; set; }
    }

    public class ResponseBody
    {
        public int Credit { get; set; }
    }
}
