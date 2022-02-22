using System.Collections.Generic;

namespace VCS.Models.ViewModels
{
    public class RepositoryViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string OwnerId { get; set; }

        public List<CommitsViewModel> Commits { get; set; }

        public List<PullRequestsViewModel> PullRequests { get; set; }

        public List<IssuesViewModel> Issues { get; set; }
    }
}
