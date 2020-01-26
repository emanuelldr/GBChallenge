
namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class AdicionarCompraResponse : BaseResponse
    {
        public AdicionarCompraResponse(string mensagem, int codigoRetorno) : base(mensagem, false, codigoRetorno)
        {
        }

        public AdicionarCompraResponse() : base()
        {
        }
    }
}
