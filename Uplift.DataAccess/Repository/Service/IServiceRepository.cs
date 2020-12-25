using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using Uplift.Models;

namespace Uplift.DataAccess.Repository
{
    public interface IServiceRepository: IRepository<Service>
    {
        Task Update(Service service);
    }
}