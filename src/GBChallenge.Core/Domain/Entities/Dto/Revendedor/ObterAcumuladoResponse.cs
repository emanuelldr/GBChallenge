
namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class ObterAcumuladoResponse : BaseResponse
    {
        public int Credito { get; }

        public ObterAcumuladoResponse(string mensagem, int codigoRetorno) : base(mensagem, false, codigoRetorno)
        {
        }

        public ObterAcumuladoResponse(int credito) : base()
        {
            Credito = credito;
        }
    }
}
