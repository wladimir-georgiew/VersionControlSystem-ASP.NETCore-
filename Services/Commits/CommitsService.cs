using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCS.Data;
using VCS.Data.Models;
using VCS.Models.InputModels;
using VCS.Models.ViewModels;

namespace VCS.Services.Commits
{
    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IQueryable<Commit> GetAll() => this.db.Commits;

        public IQueryable<Commit> GetRepositoryCommits(int repoId) => this.db.Commits.Where(x => x.RepositoryId == repoId)
            .OrderByDescending(x => x.CreatedOn);

        public List<CommitsViewModel> GetCommitsViewModel(IQueryable<Commit> commits)
        {
            var model = commits
                
                .Select(x => new CommitsViewModel
            {
                Title = x.Title,
                Description = x.Description,
                CreatedOn = x.CreatedOn.ToString("d"),
                User = x.User.UserName,
            })
                .ToList();

            return model;
        }

        public Commit CreateCommit(CommitInputModel input, string userId)
        {
            var commit = new Commit
            {
                Title = input.Title,
                Description = input.Description,
                CreatedOn = DateTime.UtcNow,
                UserId = userId,
                RepositoryId = input.Id,
            };

            return commit;
        }

        public async Task AddCommitAsync(Commit commit)
        {
            await this.db.Commits.AddAsync(commit);
            await this.db.SaveChangesAsync();
        }
    }
}
