using System.Threading.Tasks;
using GBChallenge.Core.Domain.Entities;

namespace GBChallenge.Core.Domain.Interfaces
{
    public interface ICompraRepository
    {
        Task Atualizar(Compra compra);
        Task Excluir(Compra compra);
        Task Inserir(Compra compra);
        Task Obter(Compra compra);
        Task<Compra> Listar();
        Task<Compra> Listar(string CPFRevendedor);
    }
}
