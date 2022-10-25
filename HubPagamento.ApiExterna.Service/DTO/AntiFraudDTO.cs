using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.DTO
{
    public class AntiFraudDTO
    {
        /// <summary>
        /// Endereço IP do cliente
        /// </summary>
        /// <example>
        /// 177.139.52.16
        /// </example>
        [JsonPropertyName("clienteIP")]
        public string CustomerIP { get; set; }

        /// <summary>
        /// Fingerprint de dispositivo do cliente
        /// </summary>
        /// <example>
        /// 4738d516f09cab3a2c1ee973bec88a5a367a59e4
        /// </example>
        public string Device { get; set; }

        /// <summary>
        /// Nome do Comprador
        /// </summary>
        /// <example>Charles Xavier</example>
        [JsonPropertyName("nomeComprador")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Número de documento do Cliente
        /// </summary>
        /// <example>121212121</example>
        [JsonPropertyName("documento")]
        public string Document { get; set; }

        /// <summary>
        /// DDD do Telefone do Cliente
        /// </summary>
        /// <example>21</example>
        [JsonPropertyName("telefoneDDD")]
        public string PhoneDDD { get; set; }

        /// <summary>
        /// Número de Telefone do Cliente
        /// </summary>
        /// <example>2121-8800</example>
        [JsonPropertyName("telefoneNumero")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Moeda do Pagamento
        /// </summary>
        /// <example>BRL</example>
        [JsonPropertyName("moeda")]
        public string Currency { get; set; }

        /// <summary>
        /// Endereço de e-mail do cliente
        /// </summary>
        /// <example>teste@teste.com</example>
        public string Email { get; set; }

        /// <summary>
        /// Lista de dados não-mapeados de MDD
        /// </summary>
        [JsonPropertyName("merchantDefinedData")]
        public MerchantMiscDataAntiFraudDTO[] MerchantMiscDataAntiFraud { get; set; }
    }
}
