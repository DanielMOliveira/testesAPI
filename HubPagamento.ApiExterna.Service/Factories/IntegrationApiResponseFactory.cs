using HubPagamento.ApiExterna.IoC.Configuration.Exceptions;
using HubPagamento.ApiExterna.Service.Contracts.Factories;
using HubPagamento.ApiExterna.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Factories
{
    public class IntegrationApiResponseFactory : IIntegrationApiResponseFactory
    {
        public async Task<BemobiM4UIntegrationResponse> BuildResponse(HttpResponseMessage httpResponseMessage)
        {
            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await BuildResponseOkAsync(httpResponseMessage);
                case HttpStatusCode.BadRequest:
                    return await BuildResponse400Async(httpResponseMessage);
                default:
                    string exMessage = string.Format("A Api da M4U/Bemobi retornou um código desconhecido na tentativa de tokenização do Cartão. Codigo retornado: {0} - {1}", (int)httpResponseMessage.StatusCode, httpResponseMessage.StatusCode);
                    throw new NotImplementedException(exMessage);
            }
        }

        private async Task<BemobiM4UIntegrationResponse> BuildResponseOkAsync(HttpResponseMessage response)
        {
            return await JsonSerializer.DeserializeAsync<BemobiM4UIntegrationResponse>(await response.Content.ReadAsStreamAsync());
        }

        private async Task<BemobiM4UIntegrationResponse> BuildResponse400Async(HttpResponseMessage response)
        {
            throw new BadRequestException(await response.Content.ReadAsStringAsync(), (int)HttpStatusCode.BadRequest);
        }
    }
}
