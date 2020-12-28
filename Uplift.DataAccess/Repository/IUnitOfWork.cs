using System;
using System.Threading.Tasks;

namespace Uplift.DataAccess.Repository
{
    public interface IUnitOfWork: IDisposable
    {
        ICategoryRepository Category { get; }
        IFrequencyRepository Frequency { get; }
        IServiceRepository Service { get; }
        IUserRepository User { get; }
        IOrderRepository Order { get; }
        Task SaveAsync();
    }
}