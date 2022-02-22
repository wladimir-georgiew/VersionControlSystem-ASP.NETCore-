using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using VCS.Models.InputModels;
using VCS.Services.Commits;
using VCS.Services.PullRequests;
using VCS.Services.Repositories;

namespace VCS.Controllers
{
    public class CommitsController : Controller
    {
        private readonly ICommitsService commitsService;
        private readonly IRepositoriesService repositoriesService;
        private readonly IPullRequestsService pullRequestsService;

        public CommitsController(
            ICommitsService commitsService,
            IRepositoriesService repositoriesService,
            IPullRequestsService pullRequestsService)
        {
            this.commitsService = commitsService;
            this.repositoriesService = repositoriesService;
            this.pullRequestsService = pullRequestsService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CommitInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var repoOwnerId = this.repositoriesService.GetRepositoryOwnerById(input.Id);

            // If the owner of the repository adds commit
            if (userId == repoOwnerId)
            {
                var commit = this.commitsService.CreateCommit(input, userId);
                await this.commitsService.AddCommitAsync(commit);

                return this.RedirectToAction(
                    nameof(RepositoriesController.Repository),
                    "Repositories",
                    routeValues: new { Id = input.Id });
            }

            // If someone else makes pull request
            else
            {
                var commit = this.commitsService.CreateCommit(input, userId);
                commit.PullRequestId = input.PullRequestId;

                var pullRequest = this.pullRequestsService.GetPullRequestById(input.PullRequestId);
                pullRequest.Commits.Add(commit);

                await this.commitsService.AddCommitAsync(commit);

                return this.RedirectToAction(
                    nameof(PullRequestsController.PullRequest),
                    "PullRequests",
                    routeValues: new { Id = input.PullRequestId });
            }
            
        }
    }
}
