using FizzWare.NBuilder;
using HubPagamento.ApiExterna.API.Application;
using HubPagamento.ApiExterna.API.DataContracs.Requests;
using HubPagamento.ApiExterna.IoC.Configuration;
using HubPagamento.ApiExterna.Service.Contracts;
using HubPagamento.ApiExterna.Service.Contracts.Factories;
using HubPagamento.ApiExterna.Service.DTO;
using HubPagamento.ApiExterna.Service.Factories;
using HubPagamento.ApiExterna.Service.Services.Card;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace IntegrationTests
{
    public class IntegrationTests
    {
        private Mock<IOptions<AppSettings>> _appSettings { get; set; }
        private Mock<HttpMessageHandler> _handlerMessage { get; set; }
        public IntegrationTests()
        {
            _appSettings = new Mock<IOptions<AppSettings>>();
            _handlerMessage = new Mock<HttpMessageHandler>();
        }

        [Theory]
        [InlineData(HttpStatusCode.Conflict, false, "Ocorreu um erro de validação interna")]
        [InlineData(HttpStatusCode.Unauthorized, false, "Você não possui autorização para essa funcionalidade")]
        [InlineData(HttpStatusCode.BadRequest, false, "Sua solicitação não foi entendida pelo servidor")]
        [InlineData(HttpStatusCode.OK, true, "Cartão inserido com sucesso")]
        public async Task PostCard_Deve_Retornar_Um_StatusCode_Correspondente_Ao_Inline(HttpStatusCode statusCode, bool isSucess, string message)
        {
            var json = new JsonObject() {
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

            var appSettings = Builder<AppSettings>.CreateNew().With(x => x.WalletApi = Builder<WalletApi>.CreateNew().With(x => x.BaseURL = "http://localhost/").Build()).Build();
            _appSettings.Setup(x => x.Value).Returns(() => appSettings);

            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.AddScoped<IWalletApiResponseFactory, WalletApiResponseFactory>();
                        services.AddScoped<ICardService, CardService>();
                        services.AddSingleton(httpClient);
                        services.AddSingleton(_appSettings.Object);
                    });
                });

            var client = application.CreateClient();

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
            _appSettings.Reset();
            _handlerMessage.Reset();
        }
    }
}