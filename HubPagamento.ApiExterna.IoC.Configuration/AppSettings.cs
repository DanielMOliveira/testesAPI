using System.ComponentModel.DataAnnotations;

namespace HubPagamento.ApiExterna.IoC.Configuration
{
    public class AppSettings
    {
        [Required]
        public WorkFlowApi WorkFlowApi { get; set; }

        [Required]
        public IntegrationM4UApi IntegrationM4UApi { get; set; }

        [Required]
        public JwtSettings JwtSettings { get; set; }
}

    public class WorkFlowApi
    {
        [Required]
        public string BaseURL { get; set; }

        [Required]
        public string CardEndpoint { get; set; }
    }

    public class IntegrationM4UApi
    {
        public string M4UBaseURL { get; set; }
    }

    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
    }



}