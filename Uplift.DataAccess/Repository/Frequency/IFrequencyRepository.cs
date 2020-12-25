using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using Uplift.Models;

namespace Uplift.DataAccess.Repository
{
    public interface IFrequencyRepository: IRepository<Frequency>
    {
        IEnumerable<SelectListItem> GetFrequencyDropdown();
        Task Update(Frequency frequency);
    }
}