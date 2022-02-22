using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCS.Data.Models;
using VCS.Models.InputModels;
using VCS.Models.ViewModels;

namespace VCS.Services.Repositories
{
    public interface IRepositoriesService
    {
        public IQueryable<Repository> GetAll();

        public Repository GetRepositoryById(int id);

        public List<AllRepositoriesViewModel> GetRepositoriesViewModel();

        public List<AllRepositoriesViewModel> GetPersonalRepositoriesViewModel(string userId);

        public Task CreateRepositoryAsync(RepositoryInputModel input, string userId);

        public string GetRepositoryOwnerById(int id);
    }
}
