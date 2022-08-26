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
using HubPagamento.ApiExterna.Service.Responses;

namespace HubPagamento.ApiExterna.Service.Factories
{
    public class WorkFlowApiResponseFactory : IWorkFlowApiResponseFactory
    {
        public WorkFlowApiResponseFactory()
        {

        }

        public async Task<BaseResponse> BuildResponse(HttpResponseMessage response)
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
                case HttpStatusCode.NotFound:
                    return await BuildResponse404Async(response);
                default:
                    string exMessage = string.Format("A Api de Pagamento retornou um código desconhecido.Codigo retornado: {0} - {1}", (int)response.StatusCode, response.StatusCode);
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

        private async Task<BaseResponse> BuildResponse401Async(HttpResponseMessage response)
        {
            throw new UnauthorizedAccessException();
        }
        private async Task<BaseResponse> BuildResponse400Async(HttpResponseMessage response)
        {
            throw new BadRequestException(await response.Content.ReadAsStringAsync(), (int)HttpStatusCode.BadRequest);
        }

        private async Task<BaseResponse> BuildResponse409Async(HttpResponseMessage response)
        {
            var resp = new BaseResponse()
            {
                IsSucess = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode,
                Result = await response.Content.ReadAsStringAsync()
            };

            return resp;
        }

        private async Task<BaseResponse> BuildResponse404Async(HttpResponseMessage response)
        {
            throw new NotFoundException(await response.Content.ReadAsStringAsync(), (int)HttpStatusCode.NotFound);
        }
    }
}
