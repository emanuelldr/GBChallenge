using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Entities.Dto;
using System.Threading.Tasks;

namespace GBChallenge.Core.Domain.Interfaces
{
    public interface ICompraService
    {
        Task<AdicionarCompraResponse> Adicionar(Compra compra, string cpfCompra, string cpfToken);
        Task<AtualizarCompraResponse> Atualizar(Compra compra, string cpfToken);
        Task<ExcluirCompraResponse> Excluir(int id, string cpfToken);
        Task<ListarComprasResponse> Listar(string cpfRevendedor);
    }
}
