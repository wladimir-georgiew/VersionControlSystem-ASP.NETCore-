using System.ComponentModel.DataAnnotations;
using VCS.Common;

namespace VCS.Models.InputModels
{
    public class RepositoryInputModel
    {
        [Required(ErrorMessage = GlobalConstants.ErrorMessage_MinLength_Name)]
        [MinLength(5, ErrorMessage = GlobalConstants.ErrorMessage_MinLength_Name)]
        [MaxLength(20, ErrorMessage = GlobalConstants.ErrorMessage_MaxLength_Name)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Visibility")]
        public bool IsPublic { get; set; }
    }
}
