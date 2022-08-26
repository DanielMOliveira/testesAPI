using HubPagamento.ApiExterna.IoC.Configuration.Exceptions;
using HubPagamento.ApiExterna.Service.Contracts.Factories;
using HubPagamento.ApiExterna.Service.Response;
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
    public class LoginApiResponseFactory : ILoginApiResponseFactory
    {
        public async Task<BaseResponse> BuildResponse(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await BuildResponseOkAsync(response);
                case HttpStatusCode.Unauthorized:
                    return await BuildResponse401Async(response);
                case HttpStatusCode.BadRequest:
                    return await BuildResponse422Async(response);
                case HttpStatusCode.Conflict:
                    return await BuildResponse409Async(response);
                default:
                    string exMessage = string.Format("A Api de Login retornou um código desconhecido.Codigo retornado: {0} - {1}", (int)response.StatusCode, response.StatusCode);
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
        private async Task<BaseResponse> BuildResponse422Async(HttpResponseMessage response)
        {
            var resp = new BaseResponse()
            {
                IsSucess = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode,
                Result = await response.Content.ReadAsStringAsync()
            };

            return resp;
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
    }
}
