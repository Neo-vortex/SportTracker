using MediatR;
using SportTracker.Models.Dto;
using SportTracker.Models.Types;

namespace SportTracker.Services.Commands;

public class AddActivityCommand : IRequest<RequestResult<string>>
{
    public AddActivityCommand(UserActivityDto userActivityDto, string userId)
    {
        UserActivityDto = userActivityDto;
        UserId = userId;
    }

    public UserActivityDto UserActivityDto { get; set; }
    
    public string UserId { get; set; }
}