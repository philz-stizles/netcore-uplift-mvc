using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Uplift.Models;

namespace Uplift.DataAccess.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public async Task UnLockUser(string id)
        {
            var target = await _db.AppUsers.FirstOrDefaultAsync(c => c.Id == id);
            target.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }

        public async Task LockUser(string id)
        {
            var target = await _db.AppUsers.FirstOrDefaultAsync(c => c.Id == id);
            target.LockoutEnd = DateTime.Now.AddYears(1000);
            _db.SaveChanges();
        }
    }
}