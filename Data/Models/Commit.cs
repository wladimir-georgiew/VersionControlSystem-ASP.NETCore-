using System;
using VCS.Data.Models.Common;

namespace VCS.Data.Models
{
    public class Commit : IEntity
    {
        public Commit()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        // Entity info
        public DateTime CreatedOn { get; set; }

        // Class info
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public Repository Repository { get; set; }
        public int RepositoryId { get; set; }

        public PullRequest PullRequest { get; set; }
        public int? PullRequestId { get; set; }
    }
}
