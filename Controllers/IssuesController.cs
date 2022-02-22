using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using VCS.Models.InputModels;
using VCS.Models.ViewModels;
using VCS.Services.Issues;
using System.Linq;
using VCS.Models;
using VCS.Data.Models;

namespace VCS.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IIssuesService issuesService;

        public IssuesController(IIssuesService issuesService)
        {
            this.issuesService = issuesService;
        }

        public IActionResult Issue(int id)
        {
            var issue = this.issuesService.GetIssueById(id);
            var comments = this.issuesService.GetIssueCommentsById(id).OrderByDescending(x => x.CreatedOn);

            var model = this.issuesService.GetIssueMasterModel(id, comments, issue);

            return this.View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(IssueInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var issue = this.issuesService.CreateIssue(input, userId);

            await this.issuesService.AddIssueAsync(issue);

            return this.RedirectToAction(
                nameof(this.Issue),
                routeValues: new { Id = issue.Id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostComment(IssueMasterModel masterModel)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var input = masterModel.InputModel;

            if (this.ModelState.IsValid)
            {
                var comment = this.issuesService.CreateComment(input, userId);

                await this.issuesService.AddCommentAsync(comment);
            }

            return this.RedirectToAction(
                   nameof(this.Issue),
                   routeValues: new { Id = input.IssueId });
          
        }
    }
}
