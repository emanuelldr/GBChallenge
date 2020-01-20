using System.Collections.Generic;

namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class RegistrarResponse : BaseResponse
    {
        public Token Token { get; }

        public RegistrarResponse(Token token, bool success = false, string message = null) : base(success, message)
        {
            Token = token;
        }
    }
}


