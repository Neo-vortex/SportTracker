using MediatR;
using SportTracker.Models;
using SportTracker.Models.Types;

namespace SportTracker.Services.Queries;

public class GetProfileQuery : IRequest<RequestResult<ApplicationUser>>
{
    public GetProfileQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; }
}