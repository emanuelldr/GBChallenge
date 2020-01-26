
namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class ObterRevendedorResponse : BaseResponse
    {
        public RevendedorDto Revendedor { get; }

        public ObterRevendedorResponse(string mensagem, int codigoRetorno) : base(mensagem, false, codigoRetorno)
        {
        }

        public ObterRevendedorResponse(RevendedorDto revendedor) : base()
        {
            Revendedor = revendedor;
        }
    }
}
