using ParamTech.Application.Features.Users.Commands.Create.UserInsert.Request;
using ParamTech.Dtos;

namespace ParamTech.Application.Features.Users.Profiles;
public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<UsersDto,Domain.Users>().ReverseMap();  
    }
}