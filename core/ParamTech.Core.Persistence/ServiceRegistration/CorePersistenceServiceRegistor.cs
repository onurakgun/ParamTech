using ParamTech.Core.Persistence.CrosCuttingConcerns.Caching;
using ParamTech.Core.Persistence.CrosCuttingConcerns.Caching.Redis;
using ParamTech.Core.Persistence.Utilities.Ioc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
namespace ParamTech.Core.Persistence.ServiceRegistration
{
    public class CorePersistenceServiceRegistor : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            var Configuration = RedisConfigurations.Configuration();
            services.AddSingleton<IRedisConnection, RedisConnection>();
            services.AddSingleton<ICacheManager, RedisCacheManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }
    }
}
