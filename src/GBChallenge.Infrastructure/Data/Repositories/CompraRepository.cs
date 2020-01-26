using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Interfaces;
using GBChallenge.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Atualizar(Compra compra)
        {
            _context.Compras.Update(compra);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(Compra compra)
        {
             _context.Compras.Remove(compra);
            await _context.SaveChangesAsync();
        }
         
        public async Task Inserir(Compra compra)
        {
            await _context.Compras.AddAsync(compra);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Compra>> Listar(int idRevendedor)
        {
            var result = await _context.Compras
                .Where(c => c.IdRevendedor == idRevendedor)
                .ToListAsync();

            return result;
        }

        public async Task<Compra> Obter(int id)
        {
            var result = await _context.Compras
                .Where(c => c.IdRevendedor == id)
                .FirstOrDefaultAsync();

            return result;
        }
    }
}

