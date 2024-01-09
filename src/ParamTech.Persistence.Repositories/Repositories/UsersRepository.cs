using Microsoft.EntityFrameworkCore;
using ParamTech.Application.Services;
using ParamTech.Persistence.CodeFirst.Context;

namespace ParamTech.Persistence.Repositories.Repositories
{
    public class UsersRepository : EfRepostiyory<Users, ParamTechContext>, IUsersService
    {
       
        public UsersRepository(ParamTechContext context) : base(context)
        {
        }

        public async Task<IDataResult<Users>> GetUserAsync(Guid id)
        {
            //var transaction= await  _context.Database.BeginTransactionAsync();
            var response = await _context.Users.Where(t => t.Userid == id).FirstOrDefaultAsync();
            return new SuccessDataResult<Users>(data: response);
        }
    }
}
