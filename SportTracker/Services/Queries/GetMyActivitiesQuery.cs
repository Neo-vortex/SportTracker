using MediatR;
using SportTracker.Models;
using SportTracker.Models.Types;

namespace SportTracker.Services.Queries;

public class GetMyActivitiesQuery : IRequest<RequestResult<List<UserActivity>>>
{
    public GetMyActivitiesQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; }
}