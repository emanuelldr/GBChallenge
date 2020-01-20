using GBChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GBChallenge.Domain.Interfaces
{
    interface IRevendedorService
    {
        Task Registrar(Revendedor revendedor);
        Task Autenticar(string email, string senha);
    }
}
