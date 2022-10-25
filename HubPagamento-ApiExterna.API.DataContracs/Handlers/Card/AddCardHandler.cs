using HubPagamento.ApiExterna.Service.Contracts;
using MediatR;
using HubPagamento.ApiExterna.Service.Response;
using HubPagamento.ApiExterna.Service.DTO;
using HubPagamento.ApiExterna.Service.Responses;

namespace HubPagamento.ApiExterna.API.Application
{
    public class AddCardHandler : IRequestHandler<AddCardCommand, BaseResponse>
    {
        private readonly ICardService _service;
        public AddCardHandler(ICardService service)
        {
            _service = service;
        }
        public async Task<BaseResponse> Handle(AddCardCommand request, CancellationToken cancellationToken)
        {
            var cardTokens = await _service.InvokeAddCard(new AddCardDTO(request.Customer, request.Cards));

            return cardTokens;
        }
    }
}
