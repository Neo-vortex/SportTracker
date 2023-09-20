using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SportTracker.Models;


[Index(nameof(PhoneNumber), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class ApplicationUser : IdentityUser
{
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    public string Gender { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public int Age { get; set; }
    
    public string UserType { get; set; }
    

}