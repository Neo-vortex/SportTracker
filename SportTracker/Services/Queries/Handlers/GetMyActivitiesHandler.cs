using MediatR;
using Microsoft.EntityFrameworkCore;
using SportTracker.Models;
using SportTracker.Models.Types;

namespace SportTracker.Services.Queries.Handlers;

public class GetMyActivitiesHandler : IRequestHandler<GetMyActivitiesQuery, RequestResult<List<UserActivity>>>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public GetMyActivitiesHandler(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<RequestResult<List<UserActivity>>> Handle(GetMyActivitiesQuery request, CancellationToken cancellationToken)
    {
        try
        {
           var user = await _applicationDbContext.Users
                .SingleOrDefaultAsync(user => user.Id == request.UserId, cancellationToken: cancellationToken);
            if (user == null)
            {
                return new Exception("no such a user");
            }

            var activities = await _applicationDbContext.UserActivities
                .Include(activity => activity.User)
                .Where(activity => activity.User.Id == request.UserId)
                .ToListAsync(cancellationToken: cancellationToken);

            return activities;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}