﻿using HubPagamento.ApiExterna.API.DataContracs.Commands.Integration;
using HubPagamento.ApiExterna.Service.Contracts;
using HubPagamento.ApiExterna.Service.DTO;
using HubPagamento.ApiExterna.Service.Responses;
using MediatR;

namespace HubPagamento.ApiExterna.API.DataContracs.Handlers.Integration
{
    public class TokenizeCardHandler : IRequestHandler<TokenizeCardCommand, BaseResponse>
    {
        private readonly IIntegrationService _service;
        public TokenizeCardHandler(IIntegrationService service)
        {
            _service = service;
        }
        public async Task<BaseResponse> Handle(TokenizeCardCommand request, CancellationToken cancellationToken)
        {
            var tokenizedCard = await _service.InvokeBemobiM4UAsync(new CardM4UBemodiDTO(request.Pan, request.Month, request.Year, request.Partner, request.Token));
            return tokenizedCard;
        }
    }
}
