using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GBChallenge.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GBChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevendedoresController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly GBChallengeSettings _gbChallengeSettings;

        public RevendedoresController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, IOptions<GBChallengeSettings> gbChallengeSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _gbChallengeSettings = gbChallengeSettings.Value;
        }

        // POST api/values
        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(AdicionarRevendedorRequest adicionarRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e=> e.Errors));

            var user = new IdentityUser
            {
                UserName = adicionarRequest.Email,
                Email = adicionarRequest.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, adicionarRequest.Senha);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(await GerarJWT(adicionarRequest.Email));
        }

        [HttpPost("autenticar")]
        public async Task<ActionResult> Autenticar(AutenticarRevendedorRequest autenticarRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _signInManager.PasswordSignInAsync(autenticarRequest.Email, autenticarRequest.Senha, false, false);

            if (!result.Succeeded) return BadRequest("Usuario ou Senha Invalidos");

            return Ok(await GerarJWT(autenticarRequest.Email));
        }

        public async Task<string> GerarJWT(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);

            var tokeHandler = new JwtSecurityTokenHandler();
            var chaveApi = Encoding.ASCII.GetBytes(_gbChallengeSettings.ChaveAPI);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _gbChallengeSettings.Emissor,
                Audience = _gbChallengeSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddMinutes(_gbChallengeSettings.ExpiracaoMinutos),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chaveApi), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,  usuario.Email)
                }),
            };

            return tokeHandler.WriteToken(tokeHandler.CreateToken(tokenDescriptor));
        }

    }
}
