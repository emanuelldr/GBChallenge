using System.Collections.Generic;

namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class RegistrarResponse : TokenResponse
    {
        public RegistrarResponse(string message = "", bool success = false) : base(message, success)
        {
        }

        public RegistrarResponse(Token token, string message = null, bool success = true) : base(token, message, success)
        {
        }
    }
}


   



