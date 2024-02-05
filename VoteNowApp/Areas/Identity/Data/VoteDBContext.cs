using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using VoteNowApp.Models.Model;

namespace VoteNowApp.Areas.Identity.Data;

public class VoteDBContext : IdentityDbContext<IdentityUser>
{
    public VoteDBContext(DbContextOptions<VoteDBContext> options)
        : base(options)
    {
    }
    public DbSet<CandidateModel> Candidates { get; set; }
    public DbSet<PositionModel> Positions { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<VoterModel> Voters { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
