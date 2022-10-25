using HubPagamento.ApiExterna.API.DataContracs.Commands.Integration;
using HubPagamento.ApiExterna.Service.Contracts;
using HubPagamento.ApiExterna.Service.DTO;
using HubPagamento.ApiExterna.Service.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.API.DataContracs.Handlers.Integration
{
    public class LoginM4UHandler : IRequestHandler<LoginM4UCommand, BaseResponse>
    {
        private readonly IIntegrationService _service;
        public LoginM4UHandler(IIntegrationService service)
        {
            _service = service;
        }
        public async Task<BaseResponse> Handle(LoginM4UCommand request, CancellationToken cancellationToken)
        {
            var authLogin = await _service.InvokeLoginM4UAsync(new LoginM4UDTO(request.User, request.Password));
            return authLogin;
        }
    }
}
