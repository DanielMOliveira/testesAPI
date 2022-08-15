using HubPagamento.ApiExterna.API.DataContracs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.DTO
{
    [Serializable]
    public class AddCardDTO
    {
        public AddCardDTO(CustomerDTO customer, IEnumerable<CardDTO> cards)
        {
            Customer = customer;
            Cards = cards;
        }

        public CustomerDTO Customer { get; set; }

        public IEnumerable<CardDTO> Cards { get; set; }
    }
}
