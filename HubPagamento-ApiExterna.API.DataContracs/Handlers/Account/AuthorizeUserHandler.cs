using HubPagamento.ApiExterna.API.Application;
using HubPagamento.ApiExterna.API.DataContracs.Commands.Account;
using HubPagamento.ApiExterna.Service.Contracts;
using HubPagamento.ApiExterna.Service.Response;
using HubPagamento.ApiExterna.Service.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.API.DataContracs.Handlers.Account
{
    public class AuthorizeUserHandler : IRequestHandler<AuthorizeCommand, AuthorizeResponse>
    {
        private readonly ITokenService _tokenService;
        public AuthorizeUserHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public async Task<AuthorizeResponse> Handle(AuthorizeCommand request, CancellationToken cancellationToken)
        {
            var generatedToken = await _tokenService.GenerateToken(request.Login, request.Password);

            return generatedToken;
        }
    }
}
