using HubPagamento.ApiExterna.Service.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Contracts.Factories
{
    public interface IWalletApiResponseFactory
    {
        Task<AddCardResponse> BuildResponse(HttpResponseMessage httpResponseMessage);
    }
}
