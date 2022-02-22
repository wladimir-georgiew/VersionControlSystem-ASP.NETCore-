using System;
using System.Collections.Generic;
using VCS.Data.Models.Common;

namespace VCS.Data.Models
{
    public class Repository : ISoftDeletableEntity
    {
        public Repository()
        {
            this.Commits = new HashSet<Commit>();
            this.PullRequests = new HashSet<PullRequest>();
            this.Issues = new HashSet<Issue>();
        }

        // Entity info
        public DateTime CreatedOn { get; set; }

        // Soft-deletable entity info
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        // Class info
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsPublic { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public virtual ICollection<Commit> Commits { get; set; }

        public virtual ICollection<PullRequest> PullRequests { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }
    }
}
