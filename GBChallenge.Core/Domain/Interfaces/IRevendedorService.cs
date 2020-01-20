using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GBChallenge.Core.Domain.Entities;

namespace GBChallenge.Core.Domain.Interfaces
{
    interface IRevendedorService
    {
        Task Registrar(Revendedor revendedor);
        Task Autenticar(string email, string senha);
    }
}
