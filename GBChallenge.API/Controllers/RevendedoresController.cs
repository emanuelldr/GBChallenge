using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GBChallenge.API.ViewModels;
using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GBChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevendedoresController : ControllerBase
    {
        private readonly IRevendedorService _revendedorService;
        private readonly IAutenticacaoService _autenticacaoService;

        public RevendedoresController(IRevendedorService revendedorService, IAutenticacaoService autenticacaoService)
        {
            _revendedorService = revendedorService;
            _autenticacaoService = autenticacaoService;
        }

        // POST api/values
        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(AdicionarRevendedorRequest adicionarRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var Revendedor = new Revendedor
            {

            };

            var resultado = await _revendedorService.Adicionar(Revendedor);

            if (!resultado.Successo) return BadRequest(resultado.Erros);

            return Ok();
        }

        [HttpPost("autenticar")]
        public async Task<ActionResult> Autenticar(AutenticarRevendedorRequest autenticarRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var resultado = await _autenticacaoService.Autenticar(autenticarRequest.Email, autenticarRequest.Senha);

            if(!resultado.Successo) return BadRequest("Usuario ou Senha Invalidos");

            return Ok();
        }
    }
}
