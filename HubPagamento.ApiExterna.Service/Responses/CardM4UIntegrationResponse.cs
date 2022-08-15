using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Responses
{
    public class CardM4UIntegrationResponse
    {
        [JsonPropertyName("expirationYear")]
        public int ExpirationYear { get; set; }

        [JsonPropertyName("last")]
        public string Last { get; set; }

        [JsonPropertyName("bin")]
        public string Bin { get; set; }

        [JsonPropertyName("length")]
        public int Length { get; set; }

        [JsonPropertyName("expirationMonth")]
        public int ExpirationMonth { get; set; }

        [JsonPropertyName("brand")]
        public string Brand { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}
