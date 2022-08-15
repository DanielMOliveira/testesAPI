using System.ComponentModel.DataAnnotations;

namespace HubPagamento.ApiExterna.IoC.Configuration
{
    public class AppSettings
    {
        [Required]
        public WalletApiApi WalletApi { get; set; }
    }

    public class WalletApiApi
    {
        [Required]
        public string BaseURL { get; set; }

        [Required]
        public string CardEndpoint { get; set; }
    }


}