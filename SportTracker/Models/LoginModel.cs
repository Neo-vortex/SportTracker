using System.ComponentModel.DataAnnotations;

namespace SportTracker.Models;

public class LoginModel
{
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    
    [Required]
    public string Username { get; set; }
}