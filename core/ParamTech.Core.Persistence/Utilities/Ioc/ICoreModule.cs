using Microsoft.Extensions.DependencyInjection;
namespace ParamTech.Core.Persistence.Utilities.Ioc
{
    public interface ICoreModule
    {
        void Load(IServiceCollection services);
    }
}
