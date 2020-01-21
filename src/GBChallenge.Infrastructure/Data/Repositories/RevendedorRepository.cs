using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Interfaces;
using GBChallenge.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GBChallenge.Infrastructure.Data.Repositories
{
    public class RevendedorRepository : IRevendedorRepository
    {
        private readonly GBChallengeContext _context;

        public RevendedorRepository(GBChallengeContext context)
        {
            _context = context;
        }

        public async Task Inserir(Revendedor revendedor)
        {
            await _context.Revendedores.AddAsync(revendedor);
            _context.SaveChanges();
        }

        public async Task<Revendedor> Obter(int id)
        {
            return await _context.Revendedores
                                .Where(r => r.Id == id)
                                .FirstOrDefaultAsync();
        }

        public async Task<Revendedor> Buscar(string busca)
        {
            var result = await _context.Revendedores
                                .Where(c => c.CPF == busca || c.Email == busca)
                                .FirstOrDefaultAsync();

            return result;
        }
    }
}

