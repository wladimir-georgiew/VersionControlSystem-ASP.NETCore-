using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VCS.Models.InputModels;
using VCS.Models.ViewModels;
using VCS.Services.Commits;
using VCS.Services.Issues;
using VCS.Services.PullRequests;
using VCS.Services.Repositories;

namespace VCS.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;
        private readonly ICommitsService commitsService;
        private readonly IPullRequestsService pullRequestsService;
        private readonly IIssuesService issuesService;

        public RepositoriesController(
            IRepositoriesService repositoriesService,
            ICommitsService commitsService,
            IPullRequestsService pullRequestsService,
            IIssuesService issuesService)
        {
            this.repositoriesService = repositoriesService;
            this.commitsService = commitsService;
            this.pullRequestsService = pullRequestsService;
            this.issuesService = issuesService;
        }

        public IActionResult All()
        {
            var model = this.repositoriesService.GetRepositoriesViewModel();

            return this.View(model);
        }

        [Authorize]
        public IActionResult Personal()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = this.repositoriesService.GetPersonalRepositoriesViewModel(userId);

            return this.View(model);
        }

        public IActionResult Repository(int id)
        {
            var repository = this.repositoriesService.GetRepositoryById(id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ownerId = this.repositoriesService.GetRepositoryOwnerById(id);

            if (userId != ownerId &&
                repository.IsPublic == false)
            {
                return this.Forbid();
            }

            // Commits made by the owner of the repository have null pull request and are not required to be approved
            var commits = this.commitsService.GetRepositoryCommits(id)
                .Where(x => x.PullRequest.IsApproved || x.PullRequest == null);
            //var issues = this.issuesService.GetAll();
            var issues = this.issuesService.GetRepositoryIssuesById(repository.Id);

            var pullRequestsModel = this.pullRequestsService.GetPullRequestsViewModel(id);
            var commitsModel = this.commitsService.GetCommitsViewModel(commits);
            var issuesModel = this.issuesService.GetIssuesViewModel(issues);

            var model = new RepositoryViewModel
            {
                Title = repository.Title,
                OwnerId = ownerId,
                Id = id,
                PullRequests = pullRequestsModel,
                Commits = commitsModel,
                Issues = issuesModel,
            };

            return this.View(model);
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(RepositoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.repositoriesService.CreateRepositoryAsync(input, userId);

            return this.RedirectToAction(nameof(this.Personal));
        }
    }
}
