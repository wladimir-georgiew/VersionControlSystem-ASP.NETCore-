using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCS.Models.InputModels;

namespace VCS.Models.ViewModels
{
    public class IssueViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CreatedOn { get; set; }

        public string User { get; set; }

        public List<CommentViewModel> Comments { get; set; }
    }
}
