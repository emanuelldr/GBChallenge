
namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class ExcluirCompraResponse : BaseResponse
    {
        public ExcluirCompraResponse(string mensagem, int codigoRetorno) : base(mensagem, false, codigoRetorno)
        {
        }

        public ExcluirCompraResponse() : base()
        {
        }
    }
}
