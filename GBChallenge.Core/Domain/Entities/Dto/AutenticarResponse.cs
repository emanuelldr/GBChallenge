
namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class AutenticarResponse : TokenResponse
    {
        public AutenticarResponse(string message = "", bool success = false) : base(message, success)
        {
        }

        public AutenticarResponse(Token token, string message = null, bool success = false) : base(token, message, success)
        {
        }
    }
}


