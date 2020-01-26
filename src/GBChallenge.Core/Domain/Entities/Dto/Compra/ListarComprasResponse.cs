
using System.Collections.Generic;

namespace GBChallenge.Core.Domain.Entities.Dto
{
    public class ListarComprasResponse : BaseResponse
    {
        public IEnumerable<CompraDto> Compras { get; }

        public ListarComprasResponse(string mensagem, int codigoRetorno) : base(mensagem, false, codigoRetorno)
        {
        }

        public ListarComprasResponse(IEnumerable<CompraDto> compras) : base()
        {
            Compras = compras;
        }
    }
}
