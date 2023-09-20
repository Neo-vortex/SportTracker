using MediatR;
using Microsoft.AspNetCore.Identity;
using SportTracker.Models;
using SportTracker.Models.Types;

namespace SportTracker.Services.Commands.Handler;

public class EditProfileCommandHandler : IRequestHandler<EditProfileCommand, RequestResult<bool>>
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public EditProfileCommandHandler(ApplicationDbContext applicationDbContext,
        UserManager<ApplicationUser> userManager)
    {
        _applicationDbContext = applicationDbContext;
        _userManager = userManager;
    }

    public async Task<RequestResult<bool>> Handle(EditProfileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = _applicationDbContext.Users
                .FirstOrDefault(user => user.Id == request.UserId);
            if (user == null) return new Exception("no such a user");

            user.FirstName = request.Model.FirstName;
            user.LastName = request.Model.LastName;
            user.Age = request.Model.Age;
            user.PhoneNumber = request.Model.Phone;
            user.Email = request.Model.Email;
            user.Weight = request.Model.Weight;
            user.Height = request.Model.Height;
            user.UserName = request.Model.Username;
            user.Gender = request.Model.Gender;

            if (request.Model.Password != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, request.Model.Password);
                if (!result.Succeeded) return new Exception("password reset failed");
            }

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception e)
        {
            return e;
        }
    }
}