using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VCS.Models.ViewModels
{
    public class CommentViewModel
    {
        public string User { get; set; }

        public string CreatedOn { get; set; }

        public string Text { get; set; }
    }
}
