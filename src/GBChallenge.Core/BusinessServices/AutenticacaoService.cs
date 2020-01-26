using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Entities.Dto;
using GBChallenge.Core.Domain.Entities.Settings;
using GBChallenge.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GBChallenge.Core.BusinessServices
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenSettings _tokenSettings;

        public AutenticacaoService(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, IOptions<GBChallengeSettings> gbChallengeSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenSettings = gbChallengeSettings.Value.TokenSettings;
        }

        public async Task<TokenResponse> Registrar(string cpf, string email, string senha)
        {
            var usuario = new IdentityUser
            {
                UserName = cpf,
                Email = email,
                EmailConfirmed = true
            };

            var resultado = await _userManager.CreateAsync(usuario, senha);

            if (!resultado.Succeeded)
                return new TokenResponse("Erro durante a criação do usuário: " + resultado.Errors.ToString(), 400);

            return new TokenResponse(await GerarJWT(cpf));
        }

        public async Task<TokenResponse> Autenticar(string cpf, string senha)
        {

            var resultado =
                await _signInManager.PasswordSignInAsync(cpf, senha, false,false);

            if (!resultado.Succeeded)
                return new TokenResponse("Erro durante a autenticação, senha ou login invalido", 400);

            return new TokenResponse(await GerarJWT(cpf));
        }

        private async Task<Token> GerarJWT(string claimName)
        {
            var tokeHandler = new JwtSecurityTokenHandler();
            var chaveApi = Encoding.ASCII.GetBytes(_tokenSettings.ChaveAPI);
            var expiraEm = DateTime.UtcNow.AddMinutes(_tokenSettings.ExpiracaoMinutos);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _tokenSettings.Emissor,
                Audience = _tokenSettings.ValidoEm,
                Expires = expiraEm,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chaveApi), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,  claimName)
                }),
            };

            var token = tokeHandler.WriteToken(tokeHandler.CreateToken(tokenDescriptor));
            return new Token(token, expiraEm.ToString("dd-MM-yyyy hh:mm:ss"));
        }
    }
}

