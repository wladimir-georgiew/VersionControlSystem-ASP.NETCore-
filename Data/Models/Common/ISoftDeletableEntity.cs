using System;

namespace VCS.Data.Models.Common
{
    public interface ISoftDeletableEntity : IEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
