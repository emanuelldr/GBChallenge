using System.Collections.Generic;

namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class RegistrarRevendedorResponse : TokenResponse
    {
        public RegistrarRevendedorResponse(string message = "", bool success = false) : base(message, success)
        {
        }

        public RegistrarRevendedorResponse(Token token, string message = null, bool success = true) : base(token, message, success)
        {
        }
    }
}


   



