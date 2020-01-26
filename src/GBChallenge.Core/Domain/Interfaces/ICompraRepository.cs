using System.Collections.Generic;
using System.Threading.Tasks;
using GBChallenge.Core.Domain.Entities;

namespace GBChallenge.Core.Domain.Interfaces
{
    public interface ICompraRepository
    {
        Task Atualizar(Compra compra);
        Task Excluir(Compra compra);
        Task Inserir(Compra compra);
        Task<List<Compra>> Listar(int idRevendedor);
        Task<Compra> Obter(int id);
    }
}
