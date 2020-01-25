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
        public async Task<ActionResult> Atualizar(EditarCompraRequest adicionarRequest)
        {
            return Ok();
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Excluir([FromQuery]int id)
        {
            return Ok();
        }

        [ProducesResponseType(typeof(ListarComprasResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult> Obter([FromQuery] int idCompra)
        {
            return Ok(); ;
        }

        [ProducesResponseType(typeof(ListarComprasResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet()]
        public async Task<ActionResult> Listar()
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
