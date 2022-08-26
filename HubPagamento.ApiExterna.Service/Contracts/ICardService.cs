using HubPagamento.ApiExterna.Service.DTO;
using HubPagamento.ApiExterna.Service.Response;
using HubPagamento.ApiExterna.Service.Responses;

namespace HubPagamento.ApiExterna.Service.Contracts
{
    public interface ICardService
    {
        Task<BaseResponse> InvokeAddCard(AddCardDTO infoCard);
    }
}
