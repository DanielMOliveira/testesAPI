using HubPagamento.ApiExterna.API.Application.Configuration;
using HubPagamento.ApiExterna.API.Configutation;
using HubPagamento.ApiExterna.API.DataContracs.Configutation;
using HubPagamento.ApiExterna.IoC.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Configuration
            .ConfigureProviders(builder.Environment.EnvironmentName)
            .ConfigureLogging();

    builder.Services
        .ControllersConfiguration()
        .ConfigureSwaggerVersion()
        .AddEndpointsApiExplorer()
        .ConfigureSwaggerGen();

    var appSettings = builder.ConfigureAppSettings();

    builder.Services
        .ConfigureAuthentication(appSettings)
        .AddApplication()
        .AddHttpClientConfiguration(appSettings)
        .AddMediator();

    var app = builder.Build();

    if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in app.Services.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });
    }

    _ = app.Environment.IsDevelopment()
        ? app.UseDeveloperExceptionPage()
        : app.UseMiddleware<ErrorHandlerMiddleware>();

    app.UseHsts();
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception e)
{
    Log.Fatal(e, "Falha na inicialização da aplicação.");
    throw;
}

public partial class Program { }
