using HubPagamento.ApiExterna.Service.Contracts.Factories;
using HubPagamento.ApiExterna.Service.Response;
using HubPagamento.ApiExterna.Service.Responses;
using System.Net;
using System.Text.Json;

namespace HubPagamento.ApiExterna.Service.Factories
{
    public class WorkFlowApiResponseFactory : IWorkFlowApiResponseFactory
    {
        public async Task<BaseResponse> BuildResponse(HttpResponseMessage httpResponseMessage)
        {
            return httpResponseMessage.StatusCode switch
            {
                HttpStatusCode.OK => await BuildResponseOkAsync(httpResponseMessage),
                _ => await BuildResponseDefaultAsync(httpResponseMessage),
            };
        }

        async Task<BaseResponse> BuildResponseOkAsync(HttpResponseMessage httpResponseMessage)
        {
            var walletResponse = JsonSerializer.Deserialize<WalletResponse>(await httpResponseMessage.Content.ReadAsStringAsync());
            var result = JsonSerializer.Serialize(new AddCardResponse(walletResponse));

            var resp = new BaseResponse(result)
            {
                IsSucess = httpResponseMessage.IsSuccessStatusCode,
                StatusCode = httpResponseMessage.StatusCode,
            };

            return resp;
        }

        async Task<BaseResponse> BuildResponseDefaultAsync(HttpResponseMessage httpResponseMessage)
        {
            var resp = new BaseResponse(await httpResponseMessage.Content.ReadAsStringAsync())
            {
                IsSucess = httpResponseMessage.IsSuccessStatusCode,
                StatusCode = httpResponseMessage.StatusCode,
            };

            return resp;
        }
    }
}
