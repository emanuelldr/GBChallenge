using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Entities.Dto;
using System.Threading.Tasks;

namespace GBChallenge.Core.Domain.Interfaces
{
    public interface ICompraService
    {
        Task<AdicionarCompraResponse> Adicionar(Compra compra, string cpf);
        Task<AtualizarCompraResponse> Atualizar(Compra compra);
        Task<ExcluirCompraResponse> Excluir(int id);
        Task<ListarComprasResponse> Listar(string cpfRevendedor);
    }
}
