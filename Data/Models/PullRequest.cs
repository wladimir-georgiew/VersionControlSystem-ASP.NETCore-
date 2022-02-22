using System;
using System.Collections.Generic;
using VCS.Data.Models.Common;

namespace VCS.Data.Models
{
    public class PullRequest : IEntity
    {
        public PullRequest()
        {
            this.Commits = new HashSet<Commit>();
        }
        // Entity info
        public DateTime CreatedOn { get; set; }

        // Class info
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsApproved { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public Repository Repository { get; set; }
        public int RepositoryId { get; set; }

        public virtual ICollection<Commit> Commits { get; set; }
    }
}
