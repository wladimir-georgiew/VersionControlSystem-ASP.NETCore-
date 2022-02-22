using System;
using VCS.Data.Models.Common;

namespace VCS.Data.Models
{
    public class Comment : IEntity
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        // Entity info
        public DateTime CreatedOn { get; set; }

        // Class info
        public string Id { get; set; }

        public string Text { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public Issue Issue { get; set; }
        public int IssueId { get; set; }
    }
}
