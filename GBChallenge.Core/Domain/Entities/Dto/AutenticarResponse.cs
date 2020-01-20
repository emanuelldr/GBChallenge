using System.Collections.Generic;

namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class AutenticarResponse : BaseResponse
    {
        public Token Token { get; }
        public IEnumerable<Error> Erros { get; }

        public AutenticarResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Erros = Erros;
        }

        public AutenticarResponse(Token token, bool success = false, string message = null) : base(success, message)
        {
            Token = token;
        }
    }
}


