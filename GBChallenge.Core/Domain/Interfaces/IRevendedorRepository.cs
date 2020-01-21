using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Entities.Dto;

namespace GBChallenge.Core.Domain.Interfaces
{
    public interface IRevendedorRepository
    {
        Task Inserir(Revendedor revendedor);

        Task<Revendedor> Obter(int id);

        Task<Revendedor> Buscar(string busca);
    }
}
