using ParamTech.Core.Persistence.Repositoires.Repository.Abstract;
using ParamTech.Core.Persistence.Utilities.Response;
using ParamTech.Domain;

namespace ParamTech.Application.Services;
public interface IUsersService : IAsyncRepository<Users>, IRepository<Users>
{
    Task<IDataResult<Users>> GetUserAsync(Guid id);
}
