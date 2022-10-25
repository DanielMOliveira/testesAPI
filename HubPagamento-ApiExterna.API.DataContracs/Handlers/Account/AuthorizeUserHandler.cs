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
    public class AuthorizeUserHandler : IRequestHandler<AuthorizeCommand, BaseResponse>
    {
        private readonly ILoginService _loginService;
        public AuthorizeUserHandler(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public async Task<BaseResponse> Handle(AuthorizeCommand request, CancellationToken cancellationToken)
        {
            var generatedToken = await _loginService.Login(request.Login, request.Password);

            return generatedToken;
        }
    }
}
