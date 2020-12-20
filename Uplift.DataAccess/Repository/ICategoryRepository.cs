using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using Uplift.Models;

namespace Uplift.DataAccess.Repository
{
    public interface ICategoryRepository: IRepository<Category>
    {
        IEnumerable<SelectListItem> GetCategoryDropdown();
        Task Update(Category category);
    }
}