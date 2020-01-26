
namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class BaseResponse
    {
        public bool Successo { get; }
        public string Messagem { get; }
        public int CodigoRetorno { get; }

        protected BaseResponse(string messagem = "", bool successo = true, int codigoRetorno = 200)
        {
            Successo = successo;
            Messagem = messagem;
            CodigoRetorno = codigoRetorno;
        }
    }
}

