using System.Threading.Tasks;
using Uplift.Models;

namespace Uplift.DataAccess.Repository
{
    public interface IOrderRepository: IRepository<Order>
    {
        Task ChangeStatus(int id, string status);
    }
}