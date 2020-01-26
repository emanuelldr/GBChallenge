
namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class AtualizarCompraResponse : BaseResponse
    {
        public AtualizarCompraResponse(string mensagem, int codigoRetorno) : base(mensagem, false, codigoRetorno)
        {
        }

        public AtualizarCompraResponse() : base()
        {
        }
    }
}
