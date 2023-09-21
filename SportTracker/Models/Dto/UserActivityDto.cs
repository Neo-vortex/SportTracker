using System.ComponentModel.DataAnnotations;

namespace SportTracker.Models.Dto;

public class UserActivityDto
{
    
    
    [Required]
    public int Count { get; set; }
    
    [Required]
    public int DurationMs { get; set; }
    
    [Required]
    public int Calories { get; set; }
    
    [Required]
    public int Score { get; set; }

    public UserActivity ToUserActivity()
    {
        return new UserActivity
        {
            Count = Count,
            DurationMs = DurationMs,
            Calories = Calories,
            Score = Score
        };
    }
}