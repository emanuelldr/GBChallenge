
namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class AutenticarRevendedorResponse : TokenResponse
    {
        public AutenticarRevendedorResponse(string message = "", bool success = false) : base(message, success)
        {
        }

        public AutenticarRevendedorResponse(Token token, string message = null, bool success = false) : base(token, message, success)
        {
        }
    }
}


