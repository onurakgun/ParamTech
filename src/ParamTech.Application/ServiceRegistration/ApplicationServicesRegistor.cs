using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace ParamTech.Application.ServiceRegistration;
public static class ApplicationServicesRegistor
{
    public static IServiceCollection ApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configariton => { configariton.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
        return services;
    }
}