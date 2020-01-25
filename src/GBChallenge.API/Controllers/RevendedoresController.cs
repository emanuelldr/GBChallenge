using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GBChallenge.API.Controllers.Base;
using GBChallenge.API.ViewModels;
using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Entities.Dto;
using GBChallenge.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GBChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevendedoresController : BaseController
    {
        private readonly IRevendedorService _revendedorService;
        private readonly ILogger<RevendedoresController> _logger;

        public RevendedoresController(IRevendedorService revendedorService, ILogger<RevendedoresController> logger, ILogger<BaseController> baseLogger) : base(baseLogger)
        {
            _revendedorService = revendedorService;
            _logger = logger;
        }

        // POST api/values
        [ProducesResponseType(typeof(RegistrarRevendedorResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost()]
        public async Task<ActionResult> RegistrarRevendedor(AdicionarRevendedorRequest adicionarRequest)
        {
            var Revendedor = new Revendedor
            {
                CPF = adicionarRequest.CPF,
                Email = adicionarRequest.Email,
                Nome = adicionarRequest.Nome,
                Senha = adicionarRequest.Senha
            };

            return TratarRetorno<RegistrarRevendedorResponse>(await _revendedorService.Adicionar(Revendedor), nameof(RegistrarRevendedor));
        }


        [HttpPost("autenticar")]
        [ProducesResponseType(typeof(AutenticarRevendedorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> AutenticarRevendedor(AutenticarRevendedorRequest autenticarRequest)
        {
            return TratarRetorno<AutenticarRevendedorResponse>(
                await _revendedorService.Validar(autenticarRequest.Login, autenticarRequest.Senha), 
                nameof(AutenticarRevendedor));
        }

        [Authorize]
        [ProducesResponseType(typeof(ObterAcumuladoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet("cashback")]
        public async Task<ActionResult> ObterAcumulado()
        {
            var cpf = User.Identity.Name; //cpf está contido no jwt

            return TratarRetorno<ObterAcumuladoResponse>(
                await _revendedorService.ObterAcumulado(cpf),
                nameof(AutenticarRevendedor)
                );

        }
    }
}
