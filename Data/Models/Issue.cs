using System;
using System.Collections.Generic;
using VCS.Data.Models.Common;

namespace VCS.Data.Models
{
    public class Issue : IEntity
    {
        public Issue()
        {
            this.Comments = new HashSet<Comment>();
        }

        // Entity info
        public DateTime CreatedOn { get; set; }

        // Class info
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public Repository Repository { get; set; }
        public int RepositoryId { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
