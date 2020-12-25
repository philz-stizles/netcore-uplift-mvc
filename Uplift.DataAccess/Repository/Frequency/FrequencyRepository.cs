using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Uplift.Models;

namespace Uplift.DataAccess.Repository
{
    public class FrequencyRepository : Repository<Frequency>, IFrequencyRepository
    {
        private readonly ApplicationDbContext _db;
        public FrequencyRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetFrequencyDropdown()
        {
            return _db.Frequencies.Select(c => new SelectListItem {
                Text = c.Name,
                Value = c.Id.ToString()
            });
        }


        public async Task Update(Frequency frequency)
        {
            var targetObj = await _db.Frequencies.FirstOrDefaultAsync(c => c.Id == frequency.Id);
            targetObj.Name = frequency.Name;
            targetObj.FrequencyCount = frequency.FrequencyCount;
            _db.SaveChanges();
        }
    }
}