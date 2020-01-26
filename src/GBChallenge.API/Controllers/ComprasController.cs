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


        /// <summary>
        /// Adicionar nova Compra
        /// </summary>
        [ProducesResponseType(typeof(AdicionarCompraResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Exception), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<ActionResult> AdicionarCompra(AdicionarCompraRequest adicionarRequest)
        {
            var cpfToken = User.Identity.Name; //cpf deve estar contido no jwt; Se não tiver, há erro de autenticação

            var compra = new Compra
            {
                Codigo = adicionarRequest.Codigo,
                Valor = adicionarRequest.Valor,
                Data = adicionarRequest.Data,          
            };
            
            return TratarRetorno<AdicionarCompraResponse>(
                await _compraService.Adicionar(compra, adicionarRequest.CPFRevendedor, cpfToken),
                nameof(AdicionarCompra));
        }

        /// <summary>
        /// Atualizar Compra
        /// </summary>
        [ProducesResponseType(typeof(AtualizarCompraResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string),(int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Exception), (int)HttpStatusCode.InternalServerError)]
        [HttpPatch("{id}")]
        public async Task<ActionResult> AtualizarCompra(int id, EditarCompraRequest adicionarRequest)
        {
            var cpfToken = User.Identity.Name; //cpf deve estar contido no jwt; Se não tiver, há erro de autenticação

            var compra = new Compra
            {
                Codigo = adicionarRequest.Codigo,
                Valor = adicionarRequest.Valor,
                Data = adicionarRequest.Data,
                Id = id
            };
          
            return TratarRetorno<AtualizarCompraResponse>(
                await _compraService.Atualizar(compra, cpfToken),
                nameof(AtualizarCompra));
        }


        /// <summary>
        /// Excluir Compra
        /// </summary>
        [ProducesResponseType(typeof(ExcluirCompraResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Exception), (int)HttpStatusCode.InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirCompra(int id)
        {
            var cpfToken = User.Identity.Name; //cpf deve estar contido no jwt; Se não tiver, há erro de autenticação

            return TratarRetorno<ExcluirCompraResponse>(
                await _compraService.Excluir(id, cpfToken),
                nameof(ExcluirCompra));
        }


        /// <summary>
        /// Listar compras do Revendedor
        /// </summary>
        [ProducesResponseType(typeof(ListarComprasResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Exception), (int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<ActionResult> ListarCompras()
        {
            var cpfToken = User.Identity.Name; //cpf deve estar contido no jwt; Se não tiver, há erro de autenticação

            return TratarRetorno<ListarComprasResponse>(
                await _compraService.Listar(cpfToken),
                nameof(ListarCompras));
        }
    }
}
