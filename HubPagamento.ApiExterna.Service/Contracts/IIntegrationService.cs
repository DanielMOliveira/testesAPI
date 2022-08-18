using HubPagamento.ApiExterna.Service.DTO;
using HubPagamento.ApiExterna.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Contracts
{
    public interface IIntegrationService
    {
        Task<BemobiM4UIntegrationResponse> InvokeBemobiM4UAsync(CardM4UBemodiDTO cardM4U);
    }
}
