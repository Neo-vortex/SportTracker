using MediatR;
using SportTracker.Models;
using SportTracker.Models.Types;

namespace SportTracker.Services.Commands;

public class EditProfileCommand : IRequest<RequestResult<bool>>
{
    public EditProfileCommand(SignupModel model, string userId)
    {
        Model = model;
        UserId = userId;
    }
    
    public string UserId { get; set; }

    public SignupModel Model { get; set; }
}