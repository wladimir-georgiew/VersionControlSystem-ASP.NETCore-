namespace VCS.Models.ViewModels
{
    public class AllRepositoriesViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Commits { get; set; }

        public int PullRequests { get; set; }

        public int Issues { get; set; }

        public string User { get; set; }

        public string CreatedOn { get; set; }
    }
}
