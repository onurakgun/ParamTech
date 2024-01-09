using ParamTech.Application.Features.Users.Commands.Create.UserInsert.Responses;
using ParamTech.Core.Persistence.Utilities.Response;
using ParamTech.Dtos;

namespace ParamTech.Application.Features.Users.Commands.Create.UserInsert.Request;
public class CreateUserCommand:IRequest<IDataResult<CreateUserResponse>>
{
    public  UsersDto UsersDto { get; set; }
}