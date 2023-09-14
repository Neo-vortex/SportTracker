using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SportTracker.Models;

public class ApplicationDbContext : IdentityDbContext
{
    public  DbSet<ApplicationUser> Users { get; set; }
    
    public DbSet<UserActivity>  UserActivities { get; set; }
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
}