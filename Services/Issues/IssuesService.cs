using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCS.Data;
using VCS.Data.Models;
using VCS.Models;
using VCS.Models.InputModels;
using VCS.Models.ViewModels;

namespace VCS.Services.Issues
{
    public class IssuesService : IIssuesService
    {
        private readonly ApplicationDbContext db;

        public IssuesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IQueryable<Issue> GetAll() => this.db.Issues;

        public IQueryable<Issue> GetRepositoryIssuesById(int repoId) =>
            this.db.Repositories
            .Where(x => x.Id == repoId)
            .SelectMany(x => x.Issues)
            .OrderByDescending(x => x.CreatedOn);

        public List<IssuesViewModel> GetIssuesViewModel(IQueryable<Issue> issues)
        {
            var model = issues.Select(x => new IssuesViewModel
            {
                Title = x.Title,
                User = x.User.UserName,
                CreatedOn = x.CreatedOn.ToString("d"),
                Id = x.Id,
            })
                .ToList();

            return model;
        }

        public IssueMasterModel GetIssueMasterModel(int id, IQueryable<Comment> comments, Issue issue)
        {
            var commentsModel = comments.Select(x => new CommentViewModel
            {
                CreatedOn = x.CreatedOn.ToString("d"),
                Text = x.Text,
                User = x.User.UserName,
            })
            .ToList();

            var viewModel = new IssueViewModel
            {
                User = this.GetIssueOwnerNameById(id),
                CreatedOn = issue.CreatedOn.ToString("d"),
                Description = issue.Description,
                Title = issue.Title,
                Comments = commentsModel,
                Id = id,
            };

            var masterModel = new IssueMasterModel
            {
                ViewModel = viewModel,
                InputModel = new CommentInputModel { },
            };

            return masterModel;
        }

        public Issue GetIssueById(int id) => this.db.Issues.FirstOrDefault(x => x.Id == id);

        public IQueryable<Comment> GetIssueCommentsById(int issueId) =>
                this.db.Issues
                .Where(x => x.Id == issueId)
                .SelectMany(x => x.Comments);

        public string GetIssueOwnerNameById(int issueId) =>
            this.db.Issues
            .Where(x => x.Id == issueId)
            .Select(x => x.User.UserName)
            .FirstOrDefault();

        public Issue CreateIssue(IssueInputModel input, string userId)
        {
            var issue = new Issue
            {
                CreatedOn = DateTime.UtcNow,
                Title = input.Title,
                Description = input.Description,
                UserId = userId,
                RepositoryId = input.Id,
            };

            return issue;
        }

        public async Task AddIssueAsync(Issue issue)
        {
            await this.db.Issues.AddAsync(issue);
            await this.db.SaveChangesAsync();
        }

        public Comment CreateComment(CommentInputModel input, string userId)
        {
            var comment = new Comment
            {
                CreatedOn = DateTime.UtcNow,
                IssueId = input.IssueId,
                Text = input.Text,
                UserId = userId,
            };

            return comment;
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await this.db.Comments.AddAsync(comment);
            await this.db.SaveChangesAsync();
        }
    }
}
