using HubPagamento.ApiExterna.Service.Contracts.Factories;
using HubPagamento.ApiExterna.Service.Responses;

namespace HubPagamento.ApiExterna.Service.Factories
{
    public class IntegrationApiResponseFactory : IIntegrationApiResponseFactory
    {
        public async Task<BaseResponse> BuildResponse(HttpResponseMessage httpResponseMessage)
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
