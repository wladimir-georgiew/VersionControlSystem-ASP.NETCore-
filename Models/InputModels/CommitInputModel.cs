using System.ComponentModel.DataAnnotations;
using VCS.Common;

namespace VCS.Models.InputModels
{
    public class CommitInputModel
    {
        [Required(ErrorMessage = GlobalConstants.ErrorMessage_MinLength_Title)]
        [MinLength(5, ErrorMessage = GlobalConstants.ErrorMessage_MinLength_Title)]
        [MaxLength(20, ErrorMessage = GlobalConstants.ErrorMessage_MaxLength_Title)]
        public string Title { get; set; }

        [Required(ErrorMessage = GlobalConstants.ErrorMessage_MinLength_Description)]
        [MinLength(20, ErrorMessage = GlobalConstants.ErrorMessage_MinLength_Description)]
        [MaxLength(1000, ErrorMessage = GlobalConstants.ErrorMessage_MaxLength_Description)]
        public string Description { get; set; }

        public int Id { get; set; }

        public int PullRequestId { get; set; }
    }
}
