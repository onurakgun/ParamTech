using ParamTech.Persistence.CodeFirst.Context;
namespace ParamTech.Persistence.ServiceRegistration;
public static class PersistenceServicesRegistor
{
    public static IServiceCollection PersistenenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ParamTechContext>(op => op.UseSqlServer("Data Source=ANTISYA;Database=ANTISYA.PARAM;User Id=sa;Password=123;Encrypt = False"));
        return services;
    }
}