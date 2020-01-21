using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Entities.Dto;
using GBChallenge.Core.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace GBChallenge.Core.BusinessServices
{
    public class RevendedorService : IRevendedorService
    {
        private readonly IAutenticacaoService _autenticacaoService;
        private readonly IRevendedorRepository _revendedorRepository;
        private readonly ILogger<RevendedorService> _logger;

        public RevendedorService(IAutenticacaoService autenticacaoService, IRevendedorRepository revendedorRepository, ILogger<RevendedorService> logger)
        {
            _autenticacaoService = autenticacaoService;
            _revendedorRepository = revendedorRepository;
            _logger = logger;
        }

        public async Task<RegistrarRevendedorResponse> Adicionar(Revendedor revendedor)
        {
            try
            {
                if (!ValidarEmail(revendedor.Email))
                    return new RegistrarRevendedorResponse("Email Invalido");

                revendedor.CPF = LimparCPF(revendedor.CPF);
                if (!ValidarCPF(revendedor.CPF))
                    return new RegistrarRevendedorResponse("CPF Invalido");

                var token = await _autenticacaoService.Registrar(revendedor.CPF, revendedor.Email, revendedor.Senha);

                if (!token.Successo)
                    return new RegistrarRevendedorResponse(token.Messagem);

                await _revendedorRepository.Inserir(revendedor);

                var response = new RegistrarRevendedorResponse(token.Token);

                return response;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw exception;
            }
        }

        public async Task<AutenticarRevendedorResponse> Validar(string login, string senha)
        {
            try
            {
                if (!ValidarEmail(login) && !ValidarCPF(LimparCPF(login)))
                    return new AutenticarRevendedorResponse("Login com formato invalido");

                var revendedor = await _revendedorRepository.Buscar(login);

                if (revendedor == null || revendedor.Id == 0)
                    return new AutenticarRevendedorResponse("Usuário não encontrado");

                var token = await _autenticacaoService.Autenticar(revendedor.CPF, senha);

                return new AutenticarRevendedorResponse(token.Token, token.Messagem, token.Successo);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw exception;
            }
        }


        private bool ValidarEmail(string email)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
            return (rg.IsMatch(email));
        }

        private string LimparCPF(string cpf)
        {
            return cpf.Trim().Replace(".", "").Replace("-", "");
        }

        private bool ValidarCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;
            string digito;
            int soma;
            int resto;


            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

    }
}
