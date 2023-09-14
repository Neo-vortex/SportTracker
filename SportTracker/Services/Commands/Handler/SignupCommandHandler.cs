using MediatR;
using Microsoft.AspNetCore.Identity;
using SportTracker.Models;
using SportTracker.Models.Types;

namespace SportTracker.Services.Commands.Handler;

public class SignupCommandHandler : IRequestHandler<SignupCommand, RequestResult<bool>>
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public SignupCommandHandler(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
    {
        _applicationDbContext = applicationDbContext;
        _userManager = userManager;
    }

    public async Task<RequestResult<bool>> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        try
        {
            try
            {
                var user =
                    await _userManager.FindByNameAsync(request.SignupModel.Username);

                if (user != null) return new Exception("user already exists");

                user = new ApplicationUser
                {
                    UserName = request.SignupModel.Username,
                    PhoneNumber = request.SignupModel.Phone,
                    Email = request.SignupModel.Email,
                    Gender = request.SignupModel.Gender,
                    Age = request.SignupModel.Age,
                    Height = request.SignupModel.Height,
                    Weight = request.SignupModel.Weight,
                    FirstName = request.SignupModel.FirstName,
                    LastName = request.SignupModel.LastName,
                    UserType = "normal"
                };

                var result =
                    await _userManager.CreateAsync(user, request.SignupModel.Password);
                if (!result.Succeeded)
                    return new Exception("User creation failed! Please check user details and try again." +
                                         Environment.NewLine + string.Join(Environment.NewLine,
                                             result.Errors.Select(err => err.Description)));

                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return result.Succeeded;
            }
            catch (Exception e)
            {
                return e;
            }
        }
        catch (Exception e)
        {
            return e;
        }
    }
}