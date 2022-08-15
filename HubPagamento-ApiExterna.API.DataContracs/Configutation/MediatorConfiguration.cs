using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HubPagamento.ApiExterna.API.Application.Configuration
{
    public static class MediatorConfiguration
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return services.AddMediatR(assembly);
        }
    }
}
