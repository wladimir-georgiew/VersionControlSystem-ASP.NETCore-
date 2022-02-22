namespace VCS.Models.ViewModels
{
    public class PullRequestsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CreatedOn { get; set; }

        public string User { get; set; }

        public bool IsApproved { get; set; }

        public int Commits { get; set; }
    }
}
