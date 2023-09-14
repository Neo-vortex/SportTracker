using MediatR;
using Microsoft.EntityFrameworkCore;
using SportTracker.Models;
using SportTracker.Models.Dto;
using SportTracker.Models.Types;

namespace SportTracker.Services.Queries.Handlers;

public class GetHighestScoreUserQueryHandler : IRequestHandler<GetHighestScoreUserQuery, RequestResult<List<HighestScoreUserDto>>>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public GetHighestScoreUserQueryHandler(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }


    public async Task<RequestResult<List<HighestScoreUserDto>>> Handle(GetHighestScoreUserQuery request, CancellationToken cancellationToken)
    {
        var highestScoreActivities = (await
                _applicationDbContext.UserActivities
                    .OrderByDescending(activity => activity.Score)
                    .Take(request.Count)
                    .Include(activity => activity.User)
                    .ToListAsync(cancellationToken: cancellationToken))
            .Select(activity => new HighestScoreUserDto()
            {
                UserName = activity.User.UserName!,
                Score = activity.Score,
                UserId = activity.User.Id
            })
            .ToList();
        
        return highestScoreActivities;
    }
}