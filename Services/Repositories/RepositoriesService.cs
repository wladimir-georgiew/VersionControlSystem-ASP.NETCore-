using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCS.Data;
using VCS.Data.Models;
using VCS.Models.InputModels;
using VCS.Models.ViewModels;

namespace VCS.Services.Repositories
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IQueryable<Repository> GetAll() => this.db.Repositories;

        public Repository GetRepositoryById(int id) => this.db.Repositories.FirstOrDefault(x => x.Id == id);

        public List<AllRepositoriesViewModel> GetRepositoriesViewModel()
        {
            var repositories = this.GetAll()
                .Where(x => x.IsDeleted == false)
                .Where(x => x.IsPublic == true);

            var model = repositories
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new AllRepositoriesViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Commits = x.Commits.Where(c => c.PullRequest.IsApproved || c.PullRequest == null).Count(),
                Issues = x.Issues.Count(),
                PullRequests = x.PullRequests.Count(),
                User = x.User.UserName,
                CreatedOn = x.CreatedOn.ToString("d"),
            })
                .ToList();

            return model;
        }

        public List<AllRepositoriesViewModel> GetPersonalRepositoriesViewModel(string userId)
        {
            var repositories = this.GetAll().Where(x => x.UserId == userId);

            var model = repositories
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new AllRepositoriesViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Commits = x.Commits.Where(c => c.PullRequest.IsApproved || c.PullRequest == null).Count(),
                Issues = x.Issues.Count(),
                PullRequests = x.PullRequests.Count(),
                User = x.User.UserName,
                CreatedOn = x.CreatedOn.ToString("d"),
            })
                .ToList();

            return model;
        }

        public async Task CreateRepositoryAsync(RepositoryInputModel input, string userId)
        {
            var repository = new Repository
            {
                Title = input.Title,
                IsPublic = input.IsPublic,
                CreatedOn = DateTime.UtcNow,
                UserId = userId,
            };

            await this.db.Repositories.AddAsync(repository);
            await this.db.SaveChangesAsync();
        }

        public string GetRepositoryOwnerById(int id)
        {
            return this.db.Repositories
                 .Select(x => new 
                 {
                     Id = x.Id,
                     OwnerId = x.UserId,
                 })
                 .FirstOrDefault(x => x.Id == id)
                 .OwnerId;
        }

        
    }
}
