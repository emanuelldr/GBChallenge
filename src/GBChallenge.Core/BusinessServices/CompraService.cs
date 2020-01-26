using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Entities.Dto;
using GBChallenge.Core.Domain.Interfaces;
using GBChallenge.Domain.SimpleTypes;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBChallenge.Core.BusinessServices
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _compraRepository;
        private readonly IRevendedorService _revendedorService;
        private readonly ILogger<CompraService> _logger;

        public CompraService(ICompraRepository compraRepository, IRevendedorService revendedorService, ILogger<CompraService> logger)
        {
            _compraRepository = compraRepository;
            _revendedorService = revendedorService;
            _logger = logger;
        }

        public async Task<AdicionarCompraResponse> Adicionar(Compra compra, string cpf)
        {
            var revendedorDb = await _revendedorService.Obter(cpf);
            
            if(!revendedorDb.Successo)
                return new AdicionarCompraResponse(revendedorDb.Messagem, revendedorDb.CodigoRetorno);

            compra.Status = revendedorDb.Revendedor.CompraAutoAprovada ? StatusCompra.Aprovado : StatusCompra.EmValidacao;
            compra.IdRevendedor = revendedorDb.Revendedor.Id;

            if(!ValidarCompra(compra))
                return new AdicionarCompraResponse("Codigo ou Valor da compra Invalidos", 400);

            await _compraRepository.Inserir(compra);

            return new AdicionarCompraResponse();

        }

        public async Task<AtualizarCompraResponse> Atualizar(Compra compra)
        {
            if (compra == null)
                return new AtualizarCompraResponse("Compra Invalida", 400);

            var compraDb = await _compraRepository.Obter(compra.Id);
            
            if(compraDb?.Id == null)
                new ExcluirCompraResponse("Compra não encontrada", 404);

            if(compraDb.Status != StatusCompra.EmValidacao)
                new ExcluirCompraResponse("Não é possível alterar a compra~, compra não está mais em validação.", 400);

            await _compraRepository.Atualizar(compra);

            return new AtualizarCompraResponse();
        }

        public async Task<ExcluirCompraResponse> Excluir(int id)
        {
            if (id <= 0)
                return new ExcluirCompraResponse("Id da compra Invalido", 400);

            var compra = await _compraRepository.Obter(id);

            if (compra?.Id == null)
                new ExcluirCompraResponse("Compra não encontrada", 404);

            await _compraRepository.Excluir(compra);

            return new ExcluirCompraResponse();
        }

        public async Task<ListarComprasResponse> Listar(string cpfRevendedor)
        {
            var retornoRevendedor = await _revendedorService.Obter(cpfRevendedor);

            if (!retornoRevendedor.Successo)
                return new ListarComprasResponse(retornoRevendedor.Messagem, retornoRevendedor.CodigoRetorno);

            var compras = await _compraRepository.Listar(retornoRevendedor.Revendedor.Id);

            return new ListarComprasResponse(Converter(compras));

        }

        private bool ValidarCompra(Compra compra)
        {
            if (compra.Valor <= 0)
                return false;

            if(string.IsNullOrWhiteSpace(compra.Codigo))
                return false;

            return true;
        }

        private CompraDto Converter(Compra compra)
        {
            return new CompraDto
            {
                Id = compra.Id,
                Codigo = compra.Codigo,
                Data = compra.Data,
                Valor = compra.Valor,
                PercentualCashBack = CalculaCashBackCompra(compra.Valor)
            };
        }

        private IEnumerable<CompraDto> Converter(IEnumerable<Compra> compras)
        {
            foreach(var compra in compras)
                yield return Converter(compra);
        }

        private int CalculaCashBackCompra(double valor)
        {
            if (valor < 1000)
                return 10;

            if (valor < 1500)
                return 15;

            return 20;            
        }

    }
}
