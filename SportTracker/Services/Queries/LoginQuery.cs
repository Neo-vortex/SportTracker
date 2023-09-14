using MediatR;
using SportTracker.Models;
using SportTracker.Models.Types;

namespace SportTracker.Services.Queries;

public class LoginQuery : IRequest<RequestResult<string>>
{
    public LoginQuery(LoginModel loginModel)
    {
        LoginModel = loginModel;
    }

    public LoginModel LoginModel { get; set; }
}