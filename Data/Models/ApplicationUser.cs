using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCS.Data.Models.Common;

namespace VCS.Data.Models
{
    public class ApplicationUser : IdentityUser, ISoftDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Comments = new HashSet<Comment>();
            this.Commits = new HashSet<Commit>();
            this.Repositories = new HashSet<Repository>();
            this.PullRequests = new HashSet<PullRequest>();
            this.Issues = new HashSet<Issue>();
        }

        // Entity info
        public DateTime CreatedOn { get; set; }

        // Soft-deletable entity info
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        // Class info
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Commit> Commits { get; set; }

        public virtual ICollection<Repository> Repositories { get; set; }

        public virtual ICollection<PullRequest> PullRequests { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }
    }
}
