using Microsoft.Extensions.DependencyInjection;
namespace ParamTech.Core.Persistence.Utilities.Ioc;
public static class ServiceTool
{
    public static IServiceProvider ServiceProvider { get; private set; }
    public static IServiceCollection Create(IServiceCollection services)
    {
        ServiceProvider = services.BuildServiceProvider();
        return services;
    }
}