using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCS.Data.Models;
using VCS.Models.InputModels;
using VCS.Models.ViewModels;

namespace VCS.Services.PullRequests
{
    public interface IPullRequestsService
    {
        public PullRequest GetPullRequestById(int id);

        public Repository GetPullRequestRepository(int pullRequestId);

        public IQueryable<PullRequest> GetPullRequestsByRepositoryId(int id);

        public Task<int> CreateAsync(PullRequestInputModel input, string userId);

        public List<PullRequestsViewModel> GetPullRequestsViewModel(int repositoryId);

        public Task MergePullRequestAsync(PullRequest pullRequest);
    }
}
