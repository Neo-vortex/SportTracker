using MediatR;
using Microsoft.EntityFrameworkCore;
using SportTracker.Models;
using SportTracker.Models.Types;

namespace SportTracker.Services.Commands.Handler;

public class SyncActivitiesCommandHandler : IRequestHandler<SyncActivitiesCommand, RequestResult<bool>>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public SyncActivitiesCommandHandler(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<RequestResult<bool>> Handle(SyncActivitiesCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _applicationDbContext.Users
                .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken: cancellationToken);
            if (user == null)
            {
                return new Exception("no such a user");
            }

            await _applicationDbContext.UserActivities
                .Include(activity => activity.User)
                .Where(activity => activity.User.Id == request.UserId)
                .ForEachAsync(activity => _applicationDbContext.UserActivities.Remove(activity), cancellationToken);
            

            var activities = request.UserActivityDtos
                .Select(userActivityDto => userActivityDto.ToUserActivity())
                .Select(activity =>
                {
                    activity.User = user;
                    return activity;
                });

            await _applicationDbContext.UserActivities.AddRangeAsync(activities, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}