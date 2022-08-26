using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.DTO
{
    /// <summary>
    /// Representa os dados do cliente
    /// </summary>
    public class CustomerDTO
    {
        /// <summary>
        /// Identificador único para criação da carteira e tokenização (BAN)
        /// </summary>
        /// <example>21978765432;85612005024</example>
        [Required]
        [JsonPropertyName("identificacaoCliente")]
        public string CustomerIdentity { get; set; }

        /// <summary>
        /// Documento do cliente junto ao banco (CPF/CNPJ)
        /// </summary>
        /// <example>08724894001</example>
        [Required]
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
        [JsonPropertyName("telefone")]
        [Required]
        public string Name { get; set; }
    }
}
