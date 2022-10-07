using HubPagamento.ApiExterna.Service.Responses;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.API.DataContracs.Commands.Integration
{
    public class LoginM4UCommand : IRequest<BaseResponse>
    {
        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
