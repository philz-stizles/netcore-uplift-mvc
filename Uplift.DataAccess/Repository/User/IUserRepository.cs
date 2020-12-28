using System.Threading.Tasks;
using Uplift.Models;

namespace Uplift.DataAccess.Repository
{
    public interface IUserRepository: IRepository<ApplicationUser>
    {
        Task LockUser(string id);
        Task UnLockUser(string id);
    }
}