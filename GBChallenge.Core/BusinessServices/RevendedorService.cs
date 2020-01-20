using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Entities.Dto;
using GBChallenge.Core.Domain.Interfaces;

namespace GBChallenge.Core.BusinessServices
{
    public class RevendedorService : IRevendedorService
    {
        private readonly IAutenticacaoService _autenticacaoService;
        private readonly IRevendedorRepository _revendedorRepository;

        public RevendedorService(IAutenticacaoService autenticacaoService, IRevendedorRepository revendedorRepository)
        {
            _autenticacaoService = autenticacaoService;
            _revendedorRepository = revendedorRepository;
        }

        public async Task<RegistrarResponse> Adicionar(Revendedor revendedor)
        {
            //TODO validar
            //TODO Inserir na tabela de revendedores

            var token  = await _autenticacaoService.Registrar(revendedor.CPF, revendedor.Email, revendedor.Senha);

            var response = new RegistrarResponse(token, true);

            return response;
        }

        public Task<AutenticarResponse> Validar(string email, string senha)
        {
            throw new NotImplementedException();
        }

    }
}
