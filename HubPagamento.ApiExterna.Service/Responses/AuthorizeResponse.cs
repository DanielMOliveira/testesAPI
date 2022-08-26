using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Responses
{
    public class AuthorizeResponse
    {
        public AuthorizeResponse()
        {

        }
        public AuthorizeResponse(string token, string nameSystem)
        {
            Token = token;
            NameSystem = nameSystem;
        }

        [JsonPropertyName("tokenSessao")]
        public string Token { get; set; }

        [JsonPropertyName("nomeSistema")]
        public string NameSystem { get; set; }
    }
}
