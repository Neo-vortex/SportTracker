using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SportTracker.Models;
using SportTracker.Models.Types;

namespace SportTracker.Services.Queries.Handlers;

public class LoginQueryHandler : IRequestHandler<LoginQuery, RequestResult<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _applicationDbContext;

    public LoginQueryHandler(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
    {
        _userManager = userManager;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<RequestResult<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == request.LoginModel.Username, cancellationToken: cancellationToken);
            if (user == null)
            {
                return new Exception("no such a user");
            }

            var result = await _userManager.CheckPasswordAsync(user, request.LoginModel.Password);
            if (!result)
            {
                return new Exception("invalid password");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var authSigningKey = "supersecretkeysupersecretkeysupersecretkeysupersecretkey"u8.ToArray();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, user.UserName!),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(ClaimTypes.NameIdentifier, user.Id)
                }),
                Expires = DateTime.UtcNow.AddYears(100),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(authSigningKey),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);
            return jwt;
        }
        catch (Exception e)
        {
            return e;
        }
    }


}