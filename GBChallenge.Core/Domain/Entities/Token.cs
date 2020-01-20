using System;
using System.Collections.Generic;
using System.Text;

namespace GBChallenge.Core.Domain.Entities
{
    public sealed class Token
    {
        public string TokenJwt { get; }
        public string ExpiraEm { get; }
        public bool Valido { get; }

        public Token(string tokenJwt, string expiraEm)
        {
            TokenJwt = tokenJwt;
            ExpiraEm = expiraEm;
            Valido = true;
        }

        public Token()
        {
            TokenJwt = string.Empty;
            ExpiraEm = string.Empty;
            Valido = false;
        }
    }
}
