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

        public async Task<AdicionarCompraResponse> Adicionar(Compra compra, string cpf, string cpfToken)
        {
            try
            {
                var revendedorDb = await _revendedorService.Obter(cpf);

                if (!revendedorDb.Successo)
                    return new AdicionarCompraResponse(revendedorDb.Messagem, revendedorDb.CodigoRetorno);

                var mesmoRevendedor = await _revendedorService.ValidarAnalogia(cpfToken, revendedorDb.Revendedor.Id);

                if (!mesmoRevendedor)
                    return new AdicionarCompraResponse("CPF Inforamdo não corresponde com o Usuário de acesso", 400);

                compra.Status = revendedorDb.Revendedor.CompraAutoAprovada ? StatusCompra.Aprovado : StatusCompra.EmValidacao;
                compra.IdRevendedor = revendedorDb.Revendedor.Id;
                compra.PercentualCashBack = CalculaCashBackCompra(compra.Valor);

                if (!ValidarCompra(compra))
                    return new AdicionarCompraResponse("Codigo ou Valor da compra Invalidos", 400);

                var compraId = await _compraRepository.Inserir(compra);

                return new AdicionarCompraResponse(compraId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<AtualizarCompraResponse> Atualizar(Compra compra, string cpfToken)
        {
            try
            {
                if (compra == null)
                    return new AtualizarCompraResponse("Compra Invalida", 400);

                var compraDb = await _compraRepository.Obter(compra.Id);

                if (compraDb?.Id == null)
                    return new AtualizarCompraResponse("Compra não encontrada", 404);


                var mesmoRevendedor = await _revendedorService.ValidarAnalogia(cpfToken, compraDb.IdRevendedor);

                if (!mesmoRevendedor)
                    return new AtualizarCompraResponse("CPF Inforamdo não corresponde com o Usuário de acesso", 400);

                if (compraDb.Status != StatusCompra.EmValidacao)
                    return new AtualizarCompraResponse("Não é possível alterar a compra~, compra não está mais em validação.", 400);

                compraDb = AtualizarDados(compraDb, compra);

                if (!ValidarCompra(compraDb))
                    return new AtualizarCompraResponse("Codigo ou Valor da compra Invalidos", 400);

                await _compraRepository.Atualizar(compraDb);

                return new AtualizarCompraResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<ExcluirCompraResponse> Excluir(int id, string cpfToken)
        {
            try
            {
                if (id <= 0)
                    return new ExcluirCompraResponse("Id da compra Invalido", 400);

                var compra = await _compraRepository.Obter(id);

                if (compra?.Id == null)
                    return new ExcluirCompraResponse("Compra não encontrada", 404);

                var mesmoRevendedor = await _revendedorService.ValidarAnalogia(cpfToken, compra.IdRevendedor);

                if (!mesmoRevendedor)
                    return new ExcluirCompraResponse("CPF Inforamdo não corresponde com o Usuário de acesso", 400);

                await _compraRepository.Excluir(compra);

                return new ExcluirCompraResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<ListarComprasResponse> Listar(string cpfRevendedor)
        {
            try
            {
                var retornoRevendedor = await _revendedorService.Obter(cpfRevendedor);

                if (!retornoRevendedor.Successo)
                    return new ListarComprasResponse(retornoRevendedor.Messagem, retornoRevendedor.CodigoRetorno);

                var compras = await _compraRepository.Listar(retornoRevendedor.Revendedor.Id);

                return new ListarComprasResponse(Converter(compras));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw ex;
            }

        }

        private bool ValidarCompra(Compra compra)
        {
            if (compra.Valor <= 0)
                return false;

            if(string.IsNullOrWhiteSpace(compra.Codigo))
                return false;

            if (compra.Data == DateTime.MinValue)
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
                PercentualCashBack = compra.PercentualCashBack
            };
        }

        private Compra AtualizarDados(Compra compraDb, Compra compraAtualizada)
        {
            compraDb.Codigo = !string.IsNullOrWhiteSpace(compraAtualizada.Codigo) ? compraAtualizada.Codigo : compraDb.Codigo;
            compraDb.Data = (compraAtualizada.Data != DateTime.MinValue) ? compraAtualizada.Data : compraDb.Data;
            compraDb.Valor = compraAtualizada.Valor;
            compraDb.PercentualCashBack = CalculaCashBackCompra(compraDb.Valor);
            return compraDb;
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
