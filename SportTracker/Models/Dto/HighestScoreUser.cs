using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportTracker.Models.Dto;

public class HighestScoreUserDto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    
    public string UserId { get; set; }
    
    public int Score { get; set; }
    
    public string UserName { get; set; }
}