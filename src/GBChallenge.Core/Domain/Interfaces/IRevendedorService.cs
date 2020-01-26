using System.Threading.Tasks;
using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Entities.Dto;

namespace GBChallenge.Core.Domain.Interfaces
{
    public interface IRevendedorService
    {
        Task<RegistrarRevendedorResponse> Adicionar(Revendedor revendedor);
        Task<AutenticarRevendedorResponse> Validar(string email, string senha);
        Task<ObterRevendedorResponse> Obter(string cpf);
        Task<ObterAcumuladoResponse> ObterAcumulado(string cpf);
    }
}
