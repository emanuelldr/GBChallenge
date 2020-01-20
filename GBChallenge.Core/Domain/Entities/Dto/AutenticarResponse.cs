using System.Collections.Generic;

namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class AutenticarResponse : BaseResponse
    {
        public Token Token { get; }

        public AutenticarResponse(Token token, bool success = false, string message = null) : base(success, message)
        {
            Token = token;
        }
    }
}


