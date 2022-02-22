using System.ComponentModel.DataAnnotations;
using VCS.Common;

namespace VCS.Models.InputModels
{
    public class IssueInputModel
    {
        [Required(ErrorMessage = GlobalConstants.ErrorMessage_MinLength_Issue_Title)]
        [MinLength(8, ErrorMessage = GlobalConstants.ErrorMessage_MinLength_Issue_Title)]
        [MaxLength(40, ErrorMessage = GlobalConstants.ErrorMessage_MaxLength_Issue_Title)]
        public string Title { get; set; }

        [Required(ErrorMessage = GlobalConstants.ErrorMessage_MinLength_Description)]
        [MinLength(20, ErrorMessage = GlobalConstants.ErrorMessage_MinLength_Description)]
        [MaxLength(1000, ErrorMessage = GlobalConstants.ErrorMessage_MaxLength_Description)]
        public string Description { get; set; }

        public int Id { get; set; }
    }
}
