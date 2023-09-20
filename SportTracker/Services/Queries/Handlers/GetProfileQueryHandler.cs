using MediatR;
using SportTracker.Models;
using SportTracker.Models.Types;

namespace SportTracker.Services.Queries.Handlers;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, RequestResult<ApplicationUser>>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public GetProfileQueryHandler(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<RequestResult<ApplicationUser>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = _applicationDbContext.Users
                .FirstOrDefault(user => user.Id == request.UserId);
            if (user == null)
            {
                return new Exception("no such a user");
            }

            return user;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}