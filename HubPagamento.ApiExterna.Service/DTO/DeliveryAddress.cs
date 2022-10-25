using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.DTO
{
    public class DeliveryAddressDTO
    {
        /// <summary>
        /// Logradouro do Endereço
        /// </summary>
        /// <example>Av. Republica do Brasil</example>
        public string Street { get; set; }

        /// <summary>
        /// Número do Endereço
        /// </summary>
        /// <example>1988</example>
        public string Number { get; set; }

        /// <summary>
        /// Complemento do Endereço
        /// </summary>
        /// <example>Fundos</example>
        public string Complement { get; set; }

        /// <summary>
        /// Cidade do cliente
        /// </summary>
        /// <example>São Paulo</example>
        public string City { get; set; }

        /// <summary>
        /// Unidade Federativa do cliente
        /// </summary>
        /// <example>SP</example>
        public string FederalUnit { get; set; }

        /// <summary>
        /// CEP do cliente
        /// </summary>
        /// <example>11233111</example>
        public string PostalCode { get; set; }

        /// <summary>
        /// País do cliente
        /// </summary>
        /// <example>BR</example>
        public string Country { get; set; }

        /// <summary>
        /// Complemento do Número do Endereço
        /// </summary>
        /// <example>Casa 2</example>
        public string NumberInformation { get; set; }

        /// <summary>
        /// 'Complemento do Complemento' do Endereço
        /// </summary>
        /// <example>Fundos</example>
        public string ComplementInformation { get; set; }
    }
}
