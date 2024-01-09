using ParamTech.Application.Services;
using ParamTech.Persistence.Repositories.Repositories;

namespace ParamTech.Persistence.Repositories.ServiceRegistration;
public static class PersistenceRepositoriesServicesRegistor
{
    public static IServiceCollection PersistenenceRepositoriesServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUsersService, UsersRepository>();
        return services;
    }
}