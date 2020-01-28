
namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class TokenResponse : BaseResponse
    {
        public Token Token { get; }

        public TokenResponse(string mensagem, int codigoRetorno) : base(mensagem, false, codigoRetorno)
        {
        }

        public TokenResponse(Token token, int codigoRetorno) : base("", true, codigoRetorno)
        {
            Token = token;
        }

        public TokenResponse(Token token) : base()
        {
            Token = token;
        }

    }
}


   



