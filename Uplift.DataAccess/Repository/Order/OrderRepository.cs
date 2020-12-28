using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Uplift.Models;

namespace Uplift.DataAccess.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public async Task ChangeStatus(int id, string status)
        {
            var targetObj = await _db.Orders.FirstOrDefaultAsync(c => c.Id == id);
            targetObj.Status = status;

            await _db.SaveChangesAsync();
        }
    }
}