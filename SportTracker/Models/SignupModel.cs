using System.ComponentModel.DataAnnotations;

namespace SportTracker.Models;

public class SignupModel
{
    
    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }

    [Required] public string Username { get; set; }

    [EmailAddress] [Required] [DataType(DataType.EmailAddress)] public string Email { get; set; }


    [Required] [DataType(DataType.PhoneNumber)] public string Phone { get; set; }


    [Required] public string Gender { get; set; }

    [Required] public decimal Weight { get; set; }

    [Required] public int Age { get; set; }

    [Required] public decimal Height { get; set; }
    
    [DataType(DataType.Password)] public string? Password { get; set; }
}