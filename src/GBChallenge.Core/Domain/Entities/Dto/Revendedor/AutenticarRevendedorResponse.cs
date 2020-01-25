
namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class AutenticarRevendedorResponse : TokenResponse
    {
        public AutenticarRevendedorResponse(string mensagem, int codigoRetorno) : base(mensagem, codigoRetorno)
        {
        }

        public AutenticarRevendedorResponse(Token token) : base(token)
        {
        }
    }
}


