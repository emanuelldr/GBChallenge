using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Entities.Dto;
using GBChallenge.Core.Domain.Interfaces;
using GBChallenge.Core.Domain.SimpleTypes;
using Microsoft.Extensions.Logging;

namespace GBChallenge.Core.BusinessServices
{
    public class RevendedorService : IRevendedorService
    {
        private readonly IAutenticacaoService _autenticacaoService;
        private readonly IRevendedorRepository _revendedorRepository;
        private readonly ICashBackClient _cashBackClient;
        private readonly ILogger<RevendedorService> _logger;

        public RevendedorService(IAutenticacaoService autenticacaoService, IRevendedorRepository revendedorRepository, 
            ICashBackClient cashBackClient, ILogger<RevendedorService> logger)
        {
            _autenticacaoService = autenticacaoService;
            _revendedorRepository = revendedorRepository;
            _cashBackClient = cashBackClient;
            _logger = logger;
        }

        public async Task<RegistrarRevendedorResponse> Adicionar(Revendedor revendedor)
        {
            try
            {
                if (!ValidarEmail(revendedor.Email))
                    return new RegistrarRevendedorResponse("Email Invalido", 400);

                revendedor.CPF = LimparCPF(revendedor.CPF);
                if (!ValidarCPF(revendedor.CPF))
                    return new RegistrarRevendedorResponse("CPF Invalido", 400);

                var token = await _autenticacaoService.Registrar(revendedor.CPF, revendedor.Email, revendedor.Senha);

                if (!token.Successo)
                    return new RegistrarRevendedorResponse(token.Messagem, 400);

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
                    return new AutenticarRevendedorResponse("Login com formato invalido", 400);

                var revendedor = await _revendedorRepository.Buscar(login);

                if (revendedor == null || revendedor.Id == 0)
                    return new AutenticarRevendedorResponse("Usuário não encontrado", 404);

                var token = await _autenticacaoService.Autenticar(revendedor.CPF, senha);

                return new AutenticarRevendedorResponse(token.Token);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw exception;
            }
        }

        public async Task<ObterRevendedorResponse> Obter(string cpf)
        {
            try
            {
                var cpfLimpo = LimparCPF(cpf);
                if (!ValidarCPF(cpfLimpo))
                    return new ObterRevendedorResponse("CPF invalido", 400);

                var revendedor = await _revendedorRepository.Buscar(cpfLimpo);

                var revDto = new RevendedorDto
                {
                    CPF = revendedor.CPF,
                    Email = revendedor.Email,
                    Id = revendedor.Id,
                    Nome = revendedor.Nome,
                    CompraAutoAprovada = AprovacaoAutomatica(revendedor.CPF)
                };

                return new ObterRevendedorResponse(revDto);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw exception;
            }
        }

        public async Task<ObterAcumuladoResponse> ObterAcumulado(string cpf)
        {
            try
            {
                var cpfLimpo = LimparCPF(cpf);
                if (!ValidarCPF(cpfLimpo))
                    return new ObterAcumuladoResponse("CPF invalido", 400);

                var revendedor = await _revendedorRepository.Buscar(cpfLimpo);

                if (revendedor == null || revendedor.Id == 0)
                    return new ObterAcumuladoResponse("Revendedor não encontrado", 404);

                var respostaClient = await _cashBackClient.ObterAcumulado(cpfLimpo);

                if (respostaClient.Body == null)
                    throw new ArgumentNullException($"Erro ao buscar o acumulado do Revendedor {cpfLimpo}");

                return new ObterAcumuladoResponse(respostaClient.Body.Credit);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw exception;
            }
        }

        private bool AprovacaoAutomatica(string cpf) 
        {
            return (CPFAprovacaoAutomatica.Valor == LimparCPF(cpf));
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
