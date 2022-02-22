using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCS.Data;
using VCS.Data.Models;
using VCS.Models.InputModels;
using VCS.Models.ViewModels;
using VCS.Services.Commits;

namespace VCS.Services.PullRequests
{
    public class PullRequestsService : IPullRequestsService
    {
        private readonly ApplicationDbContext db;
        private readonly ICommitsService commitsService;

        public PullRequestsService(
            ApplicationDbContext db,
            ICommitsService commitsService)
        {
            this.db = db;
            this.commitsService = commitsService;
        }

        public Repository GetPullRequestRepository(int pullRequestId)
        {
            return this.db.PullRequests
                .Where(x => x.Id == pullRequestId)
                .Select(x => x.Repository)
                .FirstOrDefault();
        }

        public PullRequest GetPullRequestById(int id)
        {
            return this.db.PullRequests.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<PullRequest> GetPullRequestsByRepositoryId(int id)
        {
            return this.db.PullRequests.Where(x => x.RepositoryId == id);
        }

        public List<PullRequestsViewModel> GetPullRequestsViewModel(int repositoryId)
        {
            var pullReqs = this.GetPullRequestsByRepositoryId(repositoryId)
                ?.OrderByDescending(x => x.CreatedOn);

            var model = pullReqs.Select(x => new PullRequestsViewModel
            {
                Id = x.Id,
                Title = x.Title,
                User = x.User.UserName,
                CreatedOn = x.CreatedOn.ToString("d"),
                IsApproved = x.IsApproved,
                Commits = x.Commits.Count(),
            })
                .ToList();

            return model;
        }

        // Creates the pull request with the initial commit and returns the newly created pull request Id
        public async Task<int> CreateAsync(PullRequestInputModel input, string userId)
        {
            var pullReq = new PullRequest
            {
                IsApproved = false,
                Title = input.Title,
                CreatedOn = DateTime.UtcNow,
                RepositoryId = input.Id,
                UserId = userId
            };

            var initialCommit = new Commit
            {
                Title = input.InitialCommitTitle,
                Description = input.InitialCommit,
                CreatedOn = DateTime.UtcNow,
                RepositoryId = input.Id,
                PullRequestId = pullReq.Id,
                UserId = userId,
            };

            pullReq.Commits.Add(initialCommit);

            await this.db.PullRequests.AddAsync(pullReq);
            await this.db.Commits.AddAsync(initialCommit);
            await this.db.SaveChangesAsync();

            return pullReq.Id;
        }

        public async Task MergePullRequestAsync(PullRequest pullRequest)
        {
            pullRequest.IsApproved = true;
            await this.db.SaveChangesAsync();
        }
    }
}
