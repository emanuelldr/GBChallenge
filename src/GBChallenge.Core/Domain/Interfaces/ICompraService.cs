using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Entities.Dto;
using System.Threading.Tasks;

namespace GBChallenge.Core.Domain.Interfaces
{
    public interface ICompraService
    {
        Task<RegistrarRevendedorResponse> Adicionar(Compra compra);
        Task<RegistrarRevendedorResponse> Atualizar(Compra compra);
        Task<ExcluirCompraResponse> Excluir(int id);
        Task<ListarComprasResponse> Listar(int idRevendedor);
        Task<ObterCompraResponse> Obter(int idRevendedor);
    }
}
