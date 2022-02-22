using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCS.Data.Models;
using VCS.Models.InputModels;
using VCS.Models.ViewModels;

namespace VCS.Services.Commits
{
    public interface ICommitsService
    {
        public IQueryable<Commit> GetAll();

        public IQueryable<Commit> GetRepositoryCommits(int repoId);

        public List<CommitsViewModel> GetCommitsViewModel(IQueryable<Commit> commits);

        public Commit CreateCommit(CommitInputModel input, string userId);

        public Task AddCommitAsync(Commit commit);
    }
}
