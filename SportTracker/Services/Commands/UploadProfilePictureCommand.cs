using MediatR;
using SportTracker.Models.Types;

namespace SportTracker.Services.Commands;

public class UploadProfilePictureCommand : IRequest<RequestResult<string>>
{
    public UploadProfilePictureCommand(string userId, IFormFile file)
    {
        UserId = userId;
        File = file;
    }

    public   string UserId { get; set; }
    
    public IFormFile File { get; set; }
}