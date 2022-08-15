using HubPagamento.ApiExterna.Service.DTO;
using HubPagamento.ApiExterna.Service.Response;

namespace HubPagamento.ApiExterna.Service.Contracts
{
    public interface ICardService
    {
        Task<AddCardResponse> InvokeAddCard(AddCardDTO infoCard);
    }
}
