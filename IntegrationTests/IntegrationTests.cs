using FizzWare.NBuilder;
using HubPagamento.ApiExterna.API.Application;
using HubPagamento.ApiExterna.API.DataContracs.Commands.Account;
using HubPagamento.ApiExterna.API.DataContracs.Requests;
using HubPagamento.ApiExterna.Service.DTO;
using HubPagamento.ApiExterna.Service.Responses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IntegrationTests
{
    public class IntegrationTests
    {
        readonly WebApplicationFactory<Program> _factory;
        readonly HttpClient _client;
        AuthenticationHeaderValue _authenticationHeader { get; set; }

        const string ROUTE_LOGIN = "api/Account";
        const string LOGIN_SERVICE = "ClaroFlex-Token-DuplaTokenizacao";
        const string PASSWORD_SERVICE = "!11R39e79?o?_FOM";

        public IntegrationTests()
        {
            _factory = CreateFactory();
            _client = _factory.CreateDefaultClient();
            _authenticationHeader = Login(_client).GetAwaiter().GetResult();
        }

        WebApplicationFactory<Program> CreateFactory()
        {
            return new WebApplicationFactory<Program>()
                .WithWebHostBuilder(x => 
                {
                    x.UseEnvironment("Tests");
                });
        }

        async Task<AuthenticationHeaderValue> Login(HttpClient client)
        {
            var loginRequest = new AuthorizeCommand()
            {
                Login = LOGIN_SERVICE,
                Password = PASSWORD_SERVICE
            };

            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var jsonContent = JsonSerializer.Serialize(loginRequest, jsonOptions);

            var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var loginResponse = await client.PostAsync(client.BaseAddress + ROUTE_LOGIN, requestContent);

            var authorizeResponse = await JsonSerializer.DeserializeAsync<AuthorizeResponse>(await loginResponse.Content.ReadAsStreamAsync(), jsonOptions);

            ArgumentNullException.ThrowIfNull(authorizeResponse);

            return new AuthenticationHeaderValue("Bearer", authorizeResponse.Token);
        }

        [Theory(Skip = "Pulando teste por conta do ambiente não estar pronto ainda.")]
        [InlineData(HttpStatusCode.Conflict, false)]
        [InlineData(HttpStatusCode.Unauthorized, false)]
        [InlineData(HttpStatusCode.BadRequest, false)]
        [InlineData(HttpStatusCode.OK, true)]
        public async Task PostCard_Deve_Retornar_Um_StatusCode_Correspondente_Ao_Inline(HttpStatusCode statusCode, bool isSucess)
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = _authenticationHeader;

            var cards = Builder<CardDTO>.CreateListOfSize(2).Build();
            var requestMessage = Builder<AddCardCommand>.CreateNew().With(x => x.Cards = cards).With(x => x.Customer = Builder<CustomerDTO>.CreateNew().Build()).Build();

            // Act
            var response = await _client.PostAsync("api/Card", new StringContent(JsonSerializer.Serialize(requestMessage), Encoding.UTF8, "application/json"));

            // Assert
            Assert.True(response.IsSuccessStatusCode == isSucess);
            Assert.True(response.StatusCode == statusCode);
        }
    }
}