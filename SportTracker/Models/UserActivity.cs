using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SportTracker.Models;

public class UserActivity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public int Gid { get; set; }
    public int Count { get; set; }
    public int DurationMs { get; set; }
    public int Calories { get; set; }
    public int Score { get; set; }
    
    [JsonIgnore]    
    public ApplicationUser User { get; set; }
}