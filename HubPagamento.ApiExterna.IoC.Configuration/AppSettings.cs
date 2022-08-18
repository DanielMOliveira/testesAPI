using System.ComponentModel.DataAnnotations;

namespace HubPagamento.ApiExterna.IoC.Configuration
{
    public class AppSettings
    {
        [Required]
        public WalletApi WalletApi { get; set; }

        [Required]
        public IntegrationApi IntegrationApi { get; set; }
    }

    public class WalletApi
    {
        [Required]
        public string BaseURL { get; set; }

        [Required]
        public string CardEndpoint { get; set; }
    }

    public class IntegrationApi
    {
        public string M4UBaseURL { get; set; }
    }





}