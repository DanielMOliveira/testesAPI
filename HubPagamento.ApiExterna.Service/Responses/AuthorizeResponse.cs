using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Responses
{
    public class AuthorizeResponse
    {
        public AuthorizeResponse()
        {

        }
        public AuthorizeResponse(string token, string message)
        {
            Token = token;
            Message = message;
        }

        public string Token { get; set; }
        public string Message { get; set; }
    }
}
