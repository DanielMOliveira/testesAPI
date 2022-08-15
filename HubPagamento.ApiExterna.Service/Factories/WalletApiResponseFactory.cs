using HubPagamento.ApiExterna.Service.Contracts.Factories;
using HubPagamento.ApiExterna.Service.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using HubPagamento.ApiExterna.IoC.Configuration.Exceptions;
using System.Net;

namespace HubPagamento.ApiExterna.Service.Factories
{
    public class WalletApiResponseFactory : IWalletApiResponseFactory
    {
        public WalletApiResponseFactory()
        {

        }

        public async Task<AddCardResponse> BuildResponse(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await BuildResponseOkAsync(response);
                case HttpStatusCode.Unauthorized:
                    return await BuildResponse401Async(response);
                case HttpStatusCode.BadRequest:
                    return await BuildResponse400Async(response);
                case HttpStatusCode.Conflict:
                    return await BuildResponse409Async(response);
                default:
                    string exMessage = string.Format("A Api de Pagamento retornou um código desconhecido.Codigo retornado: {0} - {1}", (int)response.StatusCode, response.StatusCode);
                    throw new NotImplementedException(exMessage);
            }
        }

        private async Task<AddCardResponse> BuildResponseOkAsync(HttpResponseMessage response)
        {
            return await JsonSerializer.DeserializeAsync<AddCardResponse>(await response.Content.ReadAsStreamAsync());
        }

        private async Task<AddCardResponse> BuildResponse401Async(HttpResponseMessage response)
        {
            throw new UnauthorizedAccessException();
        }
        private async Task<AddCardResponse> BuildResponse400Async(HttpResponseMessage response)
        {
            throw new BadRequestException(await response.Content.ReadAsStringAsync(), (int)HttpStatusCode.BadRequest);
        }

        private async Task<AddCardResponse> BuildResponse409Async(HttpResponseMessage response)
        {
            throw new ConflictException(await response.Content.ReadAsStringAsync(), (int)HttpStatusCode.Conflict);
        }
    }
}
