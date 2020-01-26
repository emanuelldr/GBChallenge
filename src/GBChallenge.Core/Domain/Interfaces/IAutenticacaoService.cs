using System.Threading.Tasks;
using GBChallenge.Core.Domain.Entities.Dto;

namespace GBChallenge.Core.Domain.Interfaces
{
    public interface IAutenticacaoService
    {
        Task<TokenResponse> Registrar(string cpf, string email, string senha);
        Task<TokenResponse> Autenticar(string cpf, string senha);
    }
}
