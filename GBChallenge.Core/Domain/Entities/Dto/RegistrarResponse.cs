using System.Collections.Generic;

namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class RegistrarResponse : BaseResponse
    {
        public Token Token { get; }
        public IEnumerable<Error> Erros { get; }

        public RegistrarResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Erros = errors;
        }

        public RegistrarResponse(Token token, bool success = false, string message = null) : base(success, message)
        {
            Token = token;
        }
    }
}


