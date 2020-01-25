using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Entities.Dto;
using GBChallenge.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GBChallenge.Core.BusinessServices
{
    public class CompraService : ICompraService
    {
        public Task<RegistrarRevendedorResponse> Adicionar(Compra compra)
        {
            throw new NotImplementedException();
        }

        public Task<RegistrarRevendedorResponse> Atualizar(Compra compra)
        {
            throw new NotImplementedException();
        }

        public Task<ExcluirCompraResponse> Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ListarComprasResponse> Listar(int idRevendedor)
        {
            throw new NotImplementedException();
        }

        public Task<ObterCompraResponse> Obter(int idRevendedor)
        {
            throw new NotImplementedException();
        }
    }
}
