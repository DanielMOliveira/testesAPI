using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.DTO
{
    public class LoginM4UDTO
    {
        public LoginM4UDTO()
        {

        }

        public LoginM4UDTO(string user, string password)
        {
            User = user;
            Password = password;
        }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("type")]
        public string Password { get; set; }
    }
}
