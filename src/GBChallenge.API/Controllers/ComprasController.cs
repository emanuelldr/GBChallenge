using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using GBChallenge.API.ViewModels;
using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Entities.Dto;
using GBChallenge.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GBChallenge.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly ICompraService _compraService;
        private readonly ILogger<ComprasController> _logger;

        public ComprasController(ICompraService compraService, ILogger<ComprasController> logger)
        {
            _compraService = compraService;
            _logger = logger;
        }


        [ProducesResponseType(typeof(AdicionarCompraResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<ActionResult> Adicionar(AdicionarCompraRequest adicionarRequest)
        {
            return Ok();
        }

        [ProducesResponseType(typeof(EditarCompraResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPatch]
        public async Task<ActionResult> Editar(EditarCompraRequest adicionarRequest)
        {
            return Ok();
        }

        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete("{idCompra}")]
        public async Task<ActionResult> Deletar([FromQuery]int idCompra)
        {
            return Ok(); ;
        }

        [ProducesResponseType(typeof(ListarComprasResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("{idCompra}")]
        public async Task<ActionResult> Listar([FromQuery] int idCompra)
        {
            return Ok(); ;
        }

        [ProducesResponseType(typeof(ObterAcumuladoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<ActionResult> ObterAcumulado()
        {
            return Ok(); ;
        }
    }
}
