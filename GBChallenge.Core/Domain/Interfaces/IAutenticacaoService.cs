using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GBChallenge.Core.Domain.Entities;
using GBChallenge.Core.Domain.Entities.Dto;
using Microsoft.AspNetCore.Identity;

namespace GBChallenge.Core.Domain.Interfaces
{
    public interface IAutenticacaoService
    {
        Task<Token> Registrar(string cpf, string email, string senha);
        Task<Token> Autenticar(string cpf, string senha);
    }
}
