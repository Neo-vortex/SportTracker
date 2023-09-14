using MediatR;
using SportTracker.Models.Dto;
using SportTracker.Models.Types;

namespace SportTracker.Services.Queries;

public class GetHighestScoreUserQuery : IRequest<RequestResult<List<HighestScoreUserDto>>>
{
    public GetHighestScoreUserQuery(int count)
    {
        Count = count;
    }

    public int Count { get; set; }
}