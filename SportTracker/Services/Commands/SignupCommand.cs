using MediatR;
using SportTracker.Models;
using SportTracker.Models.Types;

namespace SportTracker.Services.Commands;

public class SignupCommand : IRequest<RequestResult<string>>
{
    public SignupCommand(SignupModel signupModel)
    {
        SignupModel = signupModel;
    }

    public SignupModel SignupModel { get; set; }
    
}