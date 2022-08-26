using HubPagamento.ApiExterna.IoC.Configuration.Exceptions;
using HubPagamento.ApiExterna.Service.Contracts.Factories;
using HubPagamento.ApiExterna.Service.Responses;
using System.Net;
using System.Text.Json;

namespace HubPagamento.ApiExterna.Service.Factories
{
    public class IntegrationApiResponseFactory : IIntegrationApiResponseFactory
    {
        public async Task<BaseResponse> BuildResponse(HttpResponseMessage httpResponseMessage)
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

        private async Task<BaseResponse> BuildResponseOkAsync(HttpResponseMessage response)
        {
            var resp = new BaseResponse()
            {
                IsSucess = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode,
                Result = await response.Content.ReadAsStringAsync()
            };

            return resp;
        }

        private async Task<BaseResponse> BuildResponse400Async(HttpResponseMessage response)
        {
            throw new BadRequestException(await response.Content.ReadAsStringAsync(), (int)HttpStatusCode.BadRequest);
        }
    }
}
