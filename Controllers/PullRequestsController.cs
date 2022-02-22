using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VCS.Models.InputModels;
using VCS.Services.Commits;
using VCS.Services.PullRequests;
using VCS.Services.Repositories;

namespace VCS.Controllers
{
    public class PullRequestsController : Controller
    {
        private readonly IPullRequestsService pullRequestsService;
        private readonly ICommitsService commitsService;
        private readonly IRepositoriesService repositoriesService;

        public PullRequestsController(
            IPullRequestsService pullRequestsService,
            ICommitsService commitsService,
            IRepositoriesService repositoriesService)
        {
            this.pullRequestsService = pullRequestsService;
            this.commitsService = commitsService;
            this.repositoriesService = repositoriesService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PullRequestInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var pullReqId = await this.pullRequestsService.CreateAsync(input, userId);

            return this.RedirectToAction(
                nameof(PullRequestsController.PullRequest),
                "PullRequests",
                routeValues: new { Id =  pullReqId});
        }

        public IActionResult PullRequest(int id)
        {
            var repository = this.pullRequestsService.GetPullRequestRepository(id);

            this.ViewData["Repository"] = repository;

            this.ViewData["RepositoryOwnerId"] = this.repositoriesService.GetRepositoryOwnerById(repository.Id);
            this.ViewData["PullRequestId"] = id;

            this.ViewData["PullRequest"] = this.pullRequestsService.GetPullRequestById(id);

            var commits = this.commitsService.GetRepositoryCommits(repository.Id)
                .Where(x => x.PullRequest.Id == id);

            var commitsModel = this.commitsService.GetCommitsViewModel(commits);

            return this.View(commitsModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Merge(int id)
        {
            var repositoryId = this.pullRequestsService.GetPullRequestRepository(id).Id;
            var ownerId = this.repositoriesService.GetRepositoryOwnerById(repositoryId);
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ownerId != currentUserId)
            {
                return this.Forbid();
            }

            var pullRequest = this.pullRequestsService.GetPullRequestById(id);

            await this.pullRequestsService.MergePullRequestAsync(pullRequest);

            return this.RedirectToAction(
                nameof(RepositoriesController.Repository),
                "Repositories",
                routeValues: new { Id = repositoryId });
        }
    }
}
