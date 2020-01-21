using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GBChallenge.API.ViewModels;
using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Entities.Dto;
using GBChallenge.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GBChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevendedoresController : ControllerBase
    {
        private readonly IRevendedorService _revendedorService;
        private readonly ILogger<RevendedoresController> _logger;

        public RevendedoresController(IRevendedorService revendedorService, ILogger<RevendedoresController> logger)
        {
            _revendedorService = revendedorService;
            _logger = logger;
        }

        // POST api/values
        [ProducesResponseType(typeof(RegistrarRevendedorResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(AdicionarRevendedorRequest adicionarRequest)
        {
            var Revendedor = new Revendedor
            {
                CPF = adicionarRequest.CPF,
                Email = adicionarRequest.Email,
                Nome = adicionarRequest.Nome,
                Senha = adicionarRequest.Senha
            };

            var resultado = await _revendedorService.Adicionar(Revendedor);

            if (!resultado.Successo)
            {
                _logger.LogInformation("Erro ao Adicionar Revendedor", resultado.Messagem);
                return BadRequest(resultado.Messagem);
            }
            return CreatedAtAction(nameof(Registrar), resultado);
        }


        [HttpPost("autenticar")]
        [ProducesResponseType(typeof(AutenticarRevendedorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Autenticar(AutenticarRevendedorRequest autenticarRequest)
        {
            var resultado = await _revendedorService.Validar(autenticarRequest.Login, autenticarRequest.Senha);

            if (!resultado.Successo)
            {
                _logger.LogInformation("Erro ao Validar Revendedor", resultado.Messagem);
                return BadRequest(resultado.Messagem);
            }

            return Ok(resultado);
        }
    }
}
