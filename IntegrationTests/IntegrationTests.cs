using FizzWare.NBuilder;
using HubPagamento.ApiExterna.API.Application;
using HubPagamento.ApiExterna.API.DataContracs.Requests;
using HubPagamento.ApiExterna.IoC.Configuration;
using HubPagamento.ApiExterna.Service.Contracts;
using HubPagamento.ApiExterna.Service.Contracts.Factories;
using HubPagamento.ApiExterna.Service.DTO;
using HubPagamento.ApiExterna.Service.Factories;
using HubPagamento.ApiExterna.Service.Response;
using HubPagamento.ApiExterna.Service.Responses;
using HubPagamento.ApiExterna.Service.Services.Card;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace IntegrationTests
{
    public class IntegrationTests
    {
        private Mock<HttpMessageHandler> _handlerMessage { get; set; }
        private AuthenticationHeaderValue _authenticationHeader { get; set; }
        public IntegrationTests()
        {
            _handlerMessage = new Mock<HttpMessageHandler>();
            _authenticationHeader = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQ2xhcm9GbGV4LVRva2VuLUR1cGxhVG9rZW5pemFjYW8iLCJuYmYiOjE2NjEzNzQ1NTEsImV4cCI6MTY2MTM3NTE1MSwiaXNzIjoiSHViUGFnYW1lbnRvIiwiYXVkIjoiRXZlcnlvbmUifQ.YYvgCaDkkQNq-w9wE2sBH98dvHLAS5ghspuzBhvXYEw");
        }

        [Theory]
        [InlineData(HttpStatusCode.Conflict, false, "Ocorreu um erro de validação interna")]
        [InlineData(HttpStatusCode.Unauthorized, false, "Você não possui autorização para essa funcionalidade")]
        [InlineData(HttpStatusCode.BadRequest, false, "Sua solicitação não foi entendida pelo servidor")]
        [InlineData(HttpStatusCode.OK, true, "Cartão inserido com sucesso")]
        public async Task PostCard_Deve_Retornar_Um_StatusCode_Correspondente_Ao_Inline(HttpStatusCode statusCode, bool isSucess, string message)
        {
            var json = new JsonObject()
            {
                ["message"] = message
            };

            #region Arrange
            _handlerMessage
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(json.ToString())

                })
                .Verifiable();

            var httpClient = new HttpClient(_handlerMessage.Object);
            httpClient.DefaultRequestHeaders.Authorization = _authenticationHeader;

            IConfigurationSection appSettingsSection = null;
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Testing");

                    builder.ConfigureAppConfiguration((context, config) =>
                    {

                        config
                            .AddJsonFile("appsettings.Tests.json")
                            .AddEnvironmentVariables();

                        appSettingsSection = config.Build().GetSection(nameof(AppSettings));
                        if (appSettingsSection == null)
                            throw new Exception("No appsettings section has been found");

                        var appSettings = appSettingsSection.Get<AppSettings>();
                    })
                    .ConfigureServices(services =>
                    {
                        services.AddScoped<IWorkFlowApiResponseFactory, WorkFlowApiResponseFactory>();
                        services.AddScoped<ICardService, CardService>();
                        services.AddSingleton(httpClient);
                        services.Configure<AppSettings>(appSettingsSection);
                    });
                });

            var client = application.CreateClient();
            var login = new JsonObject() {
                ["login"] = "diego",
                ["password"] = "duplatokenizacao"
            };
            
            var responseLogin = await client.PostAsync("/api/v1/account", new StringContent(login.ToString(), Encoding.UTF8, "application/json"));

            var t = await responseLogin.Content.ReadAsStringAsync();
            var token = await JsonSerializer.DeserializeAsync<AuthorizeResponse>(await responseLogin.Content.ReadAsStreamAsync());
            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.Token);

            var cards = Builder<CardDTO>.CreateListOfSize(2).Build();
            var requestMessage = Builder<AddCardCommand>.CreateNew().With(x => x.Cards = cards).With(x => x.Customer = Builder<CustomerDTO>.CreateNew().Build()).Build();
            #endregion

            #region Act
            var response = await client.PostAsync("api/card", new StringContent(JsonSerializer.Serialize(requestMessage), Encoding.UTF8, "application/json"));
            #endregion

            #region Assert
            Assert.True(response.IsSuccessStatusCode == isSucess);
            Assert.True(response.StatusCode == statusCode);
            #endregion

            Reset();
        }

        private void Reset()
        {
            _handlerMessage.Reset();
        }
    }
}