using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCS.Models.InputModels;
using VCS.Models.ViewModels;

namespace VCS.Models
{
    public class IssueMasterModel
    {
        public CommentInputModel InputModel { get; set; }

        public IssueViewModel ViewModel { get; set; }
    }
}
