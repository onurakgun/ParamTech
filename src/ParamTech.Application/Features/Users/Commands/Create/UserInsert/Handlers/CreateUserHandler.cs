using ParamTech.Application.Features.Users.Commands.Create.UserInsert.Request;
using ParamTech.Application.Features.Users.Commands.Create.UserInsert.Responses;
using ParamTech.Core.Persistence.Aspect;
using ParamTech.Core.Persistence.Utilities.Response;

namespace ParamTech.Application.Features.Users.Commands.Create.UserInsert.Handlers;
public class CreateUserHandler : IRequestHandler<CreateUserCommand, IDataResult<CreateUserResponse>>
{
    #region DEPENDENCY
    private readonly IUsersService  _usersService;
    private readonly IMapper _mapper;
    public CreateUserHandler(IUsersService usersService, IMapper mapper)
    {
        _usersService = usersService;
        _mapper = mapper;
    }
    #endregion


    [TransactionAspect()]
    public async Task<IDataResult<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Domain.Users Map = _mapper.Map<Domain.Users>(request.UsersDto); 
        var response=await _usersService.AddAsync(Map);
        return new SuccessDataResult<CreateUserResponse>(data: null,message:"Kullanıcı eklendi");
    }
}
