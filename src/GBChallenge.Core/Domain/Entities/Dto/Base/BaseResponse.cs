using System;
using System.Collections.Generic;
using System.Text;

namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class BaseResponse
    {
        public bool Successo { get; }
        public string Messagem { get; }

        protected BaseResponse(bool successo = false, string messagem = null)
        {
            Successo = successo;
            Messagem = messagem;
        }
    }
}

