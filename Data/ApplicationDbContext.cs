using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VCS.Data.Models;

namespace VCS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Commit> Commits { get; set; }

        public DbSet<Issue> Issues { get; set; }

        public DbSet<PullRequest> PullRequests { get; set; }

        public DbSet<Repository> Repositories { get; set; }

    }
}
