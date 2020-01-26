using GBChallenge.Core.Domain.Entities.ClientResponses;
using System.Threading.Tasks;

namespace GBChallenge.Core.Domain.Interfaces
{
    public interface ICashBackClient
    {
        Task<ObterAcumuladoClientResponse> ObterAcumulado(string cpfRevendedor);
    }
}
