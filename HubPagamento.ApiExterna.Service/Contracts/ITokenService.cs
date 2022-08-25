using HubPagamento.ApiExterna.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Contracts
{
    public interface ITokenService
    {
        Task<AuthorizeResponse> GenerateToken(string login, string password);
    }
}
