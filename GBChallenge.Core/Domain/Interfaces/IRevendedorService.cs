using System.Threading.Tasks;
using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Entities.Dto;

namespace GBChallenge.Core.Domain.Interfaces
{
    public interface IRevendedorService
    {
        Task<RegistrarResponse> Adicionar(Revendedor revendedor);
        Task<AutenticarResponse> Validar(string email, string senha);
    }
}
