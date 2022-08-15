using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.DTO
{
    public class ProductAntiFraudDTO
    {
        /// <summary>
        /// Nome/Descrição do Produto
        /// </summary>
        /// <example>Celular Samsung S20</example>
        public string Name { get; set; }

        /// <summary>
        /// Valor de compra do Item. Não deve ser separado por virgula. R$15,00 deve ser enviado como 1500.
        /// </summary>
        /// <example>1500</example>
        public string Value { get; set; }

        /// <summary>
        /// Quantidade do Item
        /// </summary>
        /// <example>1</example>
        public string Quantity { get; set; }

        /// <summary>
        /// Código do Item
        /// </summary>
        /// <example>001</example>
        public string Code { get; set; }

        /// <summary>
        /// SKU do Item
        /// </summary>
        /// <example>SM-001</example>
        public string SKU { get; set; }
    }
}
