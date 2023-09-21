using MediatR;
using Microsoft.EntityFrameworkCore;
using SportTracker.Models;
using SportTracker.Models.Types;

namespace SportTracker.Services.Commands.Handler;


public class AddActivityCommandHandler : IRequestHandler<AddActivityCommand , RequestResult<string>>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public AddActivityCommandHandler(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<RequestResult<string>> Handle(AddActivityCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _applicationDbContext.Users
                .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken: cancellationToken);
            if (user == null)
            {
                return new Exception("no such a user");
            }

            var activity = new UserActivity()
            {
                Calories = request.UserActivityDto.Calories,
                Count = request.UserActivityDto.Count,
                Score = request.UserActivityDto.Score,
                DurationMs = request.UserActivityDto.DurationMs,
                User = user
            };
            var result = await _applicationDbContext.UserActivities.AddAsync(activity, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return result.Entity.Id;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}