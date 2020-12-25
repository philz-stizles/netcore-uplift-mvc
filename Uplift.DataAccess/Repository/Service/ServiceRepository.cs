using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Uplift.Models;

namespace Uplift.DataAccess.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private readonly ApplicationDbContext _db;
        public ServiceRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }


        public async Task Update(Service service)
        {
            var targetObj = await _db.Services.FirstOrDefaultAsync(c => c.Id == service.Id);
            targetObj.Name = service.Name;
            targetObj.Description = service.Description;
            targetObj.Price = service.Price;
            targetObj.ImageUrl = service.ImageUrl;
            targetObj.FrequencyId = service.FrequencyId;
            targetObj.CategoryId = service.CategoryId;
            _db.SaveChanges();
        }
    }
}