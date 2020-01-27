
namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class AdicionarCompraResponse : BaseResponse
    {
        public int Id { get; }
        public AdicionarCompraResponse(string mensagem, int codigoRetorno) : base(mensagem, false, codigoRetorno)
        {
        }

        public AdicionarCompraResponse(int id) : base("", true, 201)
        {
            Id = id;
        }
    }
}
