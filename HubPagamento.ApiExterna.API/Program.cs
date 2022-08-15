using HubPagamento.ApiExterna.API.Application.Configuration;
using HubPagamento.ApiExterna.API.Configutation;
using HubPagamento.ApiExterna.API.DataContracs.Configutation;
using HubPagamento.ApiExterna.IoC.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Configuration
        .ConfigureProviders(builder.Environment.EnvironmentName)
        .ConfigureLogging();

var appSettingsSection = builder.Configuration.GetSection(nameof(AppSettings));
if (appSettingsSection == null)
    throw new Exception("No appsettings section has been found");

builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();

// Add services to the container.
builder.Services.AddControllers().ConfigureApiBehaviorOptions(option =>
{
    option.InvalidModelStateResponseFactory = context =>
    {
        var response = new
        {
            action = (context.ActionDescriptor as ControllerActionDescriptor)?.ActionName,
            errors = context.ModelState.Keys.Select(currentField =>
            {
                return new
                {
                    field = currentField,
                    Messages = context.ModelState[currentField]?.Errors.Select(e => e.ErrorMessage)
                };
            })
        };

        return new BadRequestObjectResult(response);
    };
});

builder.Services.AddHttpClientConfiguration(appSettings);
builder.Services.AddApplication();

builder.Services.AddMediator();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();

public partial class Program { }
