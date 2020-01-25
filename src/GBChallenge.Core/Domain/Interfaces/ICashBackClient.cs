using GBChallenge.Core.Domain.Entities.ClientResponses;
using GBChallenge.Core.Domain.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GBChallenge.Core.Domain.Interfaces
{
    public interface ICashBackClient
    {
        Task<ObterAcumuladoClientResponse> ObterAcumulado(string cpfRevendedor);
    }
}
