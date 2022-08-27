using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Responses
{
    public class CustomerResponse
    {
        /// <summary>
        /// Identificador único para criação da carteira e tokenização (BAN)
        /// </summary>
        /// <example>85612005024</example>
        [JsonPropertyName("identificacaoCliente")]
        public string CustomerIndentity { get; set; }

        /// <summary>
        /// Documento do cliente junto ao banco (CPF/CNPJ)
        /// </summary>
        /// <example>08724894001</example>
        [JsonPropertyName("documento")]
        public string Document { get; set; }

        /// <summary>
        /// Telefone do cliente que estará associado ao identificador
        /// </summary>
        /// <example>21978765432</example>
        [JsonPropertyName("telefone")]
        public string Phone { get; set; }

        /// <summary>
        /// Nome do cliente
        /// </summary>
        /// <example>John Doe</example>
        //[Required]
        [JsonPropertyName("nome")]
        public string Name { get; set; }
    }
}
