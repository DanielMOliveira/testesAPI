using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Requests
{
    public class LoginRequest
    {
        public LoginRequest(string service, string passWord)
        {
            Service = service;
            PassWord = passWord;
        }

        [JsonPropertyName("serviço")]
        public string Service { get; set; }

        [JsonPropertyName("senha")]
        public string PassWord { get; set; }
    }
}
