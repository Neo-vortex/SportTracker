using MediatR;
using SportTracker.Models.Dto;
using SportTracker.Models.Types;

namespace SportTracker.Services.Commands;

public class SyncActivitiesCommand : IRequest<RequestResult<bool>>
{
    public SyncActivitiesCommand(List<UserActivityDto> userActivityDtos, string userId)
    {
        UserActivityDtos = userActivityDtos;
        UserId = userId;
    }

    public List<UserActivityDto> UserActivityDtos { get; set; }
    
    public string UserId { get; set; }
}