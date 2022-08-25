using HubPagamento.ApiExterna.Service.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HubPagamento.ApiExterna.API.DataContracs.Commands.Account
{
    public class AuthorizeCommand : IRequest<AuthorizeResponse>
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
