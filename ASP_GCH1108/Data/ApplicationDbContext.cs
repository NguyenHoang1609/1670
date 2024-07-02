using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ASP_GCH1108.Models;

namespace ASP_GCH1108.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ASP_GCH1108.Models.JobList> JobList { get; set; } = default!;
        public DbSet<ASP_GCH1108.Models.JobApplication> JobApplication { get; set; } = default!;
        public DbSet<ASP_GCH1108.Models.Profile> Profile { get; set; } = default!;
    }
}
