using MediatR;
using Microsoft.EntityFrameworkCore;
using SportTracker.Models;
using SportTracker.Models.Types;

namespace SportTracker.Services.Commands.Handler;

public class UploadProfilePictureCommandHandler : IRequestHandler<UploadProfilePictureCommand,RequestResult<string>>
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UploadProfilePictureCommandHandler(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<RequestResult<string>> Handle(UploadProfilePictureCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _applicationDbContext
                .Users
                .SingleOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
            if (user == null)
            {
                return new Exception("user not found");
            }

            // remove old profile picture 
            
            if (user.ProfilePictureUrl != null)
            {
                var oldFilePath = Path.Combine(Environment.CurrentDirectory ,"Images", user.ProfilePictureUrl);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }
            var fileName = Guid.NewGuid() + Path.GetExtension(request.File.FileName);
            var filePath = Path.Combine(Environment.CurrentDirectory ,"Images", fileName);
            await using var stream = new FileStream(filePath, FileMode.Create);
            await request.File.CopyToAsync(stream, cancellationToken);
            user.ProfilePictureUrl = fileName;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return fileName;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}