using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCS.Data.Models;
using VCS.Models;
using VCS.Models.InputModels;
using VCS.Models.ViewModels;

namespace VCS.Services.Issues
{
    public interface IIssuesService
    {
        public IQueryable<Issue> GetAll();

        public IQueryable<Issue> GetRepositoryIssuesById(int repoId);

        public Issue GetIssueById(int id);

        public IQueryable<Comment> GetIssueCommentsById(int issueId);

        public IssueMasterModel GetIssueMasterModel(int id, IQueryable<Comment> comments, Issue issue);

        public string GetIssueOwnerNameById(int issueId);

        public List<IssuesViewModel> GetIssuesViewModel(IQueryable<Issue> issues);

        public Issue CreateIssue(IssueInputModel input, string userId);

        public Task AddIssueAsync(Issue issue);

        public Comment CreateComment(CommentInputModel input, string userId);

        public Task AddCommentAsync(Comment comment);

    }
}
