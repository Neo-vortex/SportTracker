using Microsoft.AspNetCore.Identity;

namespace SportTracker.Models;

public class ApplicationUser : IdentityUser
{
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    public string Gender { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public int Age { get; set; }
    
    public string UserType { get; set; }
    

}