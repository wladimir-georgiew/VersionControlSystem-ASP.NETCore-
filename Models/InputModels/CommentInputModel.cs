using System.ComponentModel.DataAnnotations;
using VCS.Common;

namespace VCS.Models.InputModels
{
    public class CommentInputModel
    {
        [Required]
        public string Text { get; set; }

        public int IssueId { get; set; }
    }
}
