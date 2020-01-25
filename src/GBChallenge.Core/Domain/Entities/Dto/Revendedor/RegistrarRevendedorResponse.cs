using System.Collections.Generic;

namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class RegistrarRevendedorResponse : TokenResponse
    {
        public RegistrarRevendedorResponse(string mensagem, int codigoRetorno) : base(mensagem, codigoRetorno)
        {
        }

        public RegistrarRevendedorResponse(Token token) : base(token)
        {
        }
    }
}


   



