using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using GBChallenge.API.Controllers.Base;
using GBChallenge.API.ViewModels;
using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Entities.Dto;
using GBChallenge.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GBChallenge.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : BaseController
    {
        private readonly ICompraService _compraService;

        public ComprasController(ICompraService compraService, ILogger<BaseController> baseLogger) : base(baseLogger)
        {
            _compraService = compraService;
            //Todo Corrigir todas as validações de CPF via token/api
        }


        [ProducesResponseType(typeof(AdicionarCompraResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<ActionResult> AdicionarCompra(AdicionarCompraRequest adicionarRequest)
        {
            var cpf = User.Identity.Name; //cpf deve estar contido no jwt; Se não tiver, há erro de autenticação

            var compra = new Compra
            {
                Codigo = adicionarRequest.Codigo,
                Valor = adicionarRequest.Valor,
                Data = adicionarRequest.Data,          
            };
            
            return TratarRetorno<AdicionarCompraResponse>(
                await _compraService.Adicionar(compra, adicionarRequest.CPFRevendedor),
                nameof(AdicionarCompra));
        }

        [ProducesResponseType(typeof(AtualizarCompraResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPatch]
        public async Task<ActionResult> AtualizarCompra(EditarCompraRequest adicionarRequest)
        {
            var cpf = User.Identity.Name; //cpf deve estar contido no jwt; Se não tiver, há erro de autenticação

            var compra = new Compra
            {
                Codigo = adicionarRequest.Codigo,
                Valor = adicionarRequest.Valor,
                Data = adicionarRequest.Data
            };
          
            return TratarRetorno<AtualizarCompraResponse>(
                await _compraService.Atualizar(compra),
                nameof(AtualizarCompra));
        }

        
        [ProducesResponseType(typeof(ExcluirCompraResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirCompra([FromQuery] int id)
        {
            var cpf = User.Identity.Name; //cpf deve estar contido no jwt; Se não tiver, há erro de autenticação
            return TratarRetorno<ExcluirCompraResponse>(
                await _compraService.Excluir(id),
                nameof(ExcluirCompra));
        }


        [ProducesResponseType(typeof(ListarComprasResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet()]
        public async Task<ActionResult> ListarCompras()
        {
            var cpf = User.Identity.Name; //cpf deve estar contido no jwt; Se não tiver, há erro de autenticação

            return TratarRetorno<ListarComprasResponse>(
                await _compraService.Listar(cpf),
                nameof(ListarCompras));
        }
    }
}
