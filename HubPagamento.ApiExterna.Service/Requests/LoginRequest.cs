using System.Text.Json.Serialization;

namespace HubPagamento.ApiExterna.Service.Requests
{
    public class LoginRequest
    {
        public LoginRequest(string service, string passWord)
        {
            Service = service;
            PassWord = passWord;
        }

        [JsonPropertyName("servico")]
        public string Service { get; set; }

        [JsonPropertyName("senha")]
        public string PassWord { get; set; }
    }
}
