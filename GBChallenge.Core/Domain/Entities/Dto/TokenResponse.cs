using System.Collections.Generic;

namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class TokenResponse : BaseResponse
    {
        public Token Token { get; }

        public TokenResponse(string message = "", bool success = false) : base(success, message)
        {
        }

        public TokenResponse(Token token, string message = "", bool success = true) : base(success, message)
        {
            Token = token;
        }
    }
}


   



