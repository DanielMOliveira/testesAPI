using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Responses
{
    public class M4UIntegrationResult
    {
        [JsonPropertyName("resultHint")]
        public string ResultHint { get; set; }

        [JsonPropertyName("resultSucess")]
        public bool ResultSucess { get; set; }

        [JsonPropertyName("resultCode")]
        public int ResultCode { get; set; }

        [JsonPropertyName("resultDetailing")]
        public string ResultDetailing { get; set; }

        [JsonPropertyName("resultMessage")]
        public string ResultMessage { get; set; }

        [JsonPropertyName("properties")]
        public dynamic Properties { get; set; }      
    }
}
