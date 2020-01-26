using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Interfaces;
using GBChallenge.Infrastructure.Data.EntityFramework;
using System;
using System.Threading.Tasks;

namespace GBChallenge.Infrastructure.Data.Repositories
{
    public class CompraRepository : ICompraRepository
    {
        private readonly GBChallengeContext _context;

        public CompraRepository(GBChallengeContext context)
        {
            _context = context;
        }

        public Task Atualizar(Compra compra)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(Compra compra)
        {
            throw new NotImplementedException();
        }

        public Task Inserir(Compra compra)
        {
            throw new NotImplementedException();
        }

        public Task Obter(Compra compra)
        {
            throw new NotImplementedException();
        }

        public Task<Compra> Listar()
        {
            throw new NotImplementedException();
        }

        public Task<Compra> Listar(string CPFRevendedor)
        {
            throw new NotImplementedException();
        }
    }
}

