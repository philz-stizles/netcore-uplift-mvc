using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Uplift.Models;
using Uplift.Utility;

namespace Uplift.DataAccess.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly RoleManager<IdentityRole> _roleMgr;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userMgr,
            RoleManager<IdentityRole> roleMgr)
        {
            _roleMgr = roleMgr;
            _userMgr = userMgr;
            _db = db;
        }

    public void Initialize()
    {
        try
        {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
        }
        catch( Exception)
        {

        }

        var roles = new List<string>{
            SD.Admin,
            SD.Manager
        };

        foreach(var role in roles)
        {
            if(!_roleMgr.RoleExistsAsync(role).GetAwaiter().GetResult())
            {
                _roleMgr.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
            }
        }

        var admin = new ApplicationUser
        {
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
            EmailConfirmed = true
        };

        _userMgr.CreateAsync(admin, "P@ssw0rd").GetAwaiter().GetResult();
        _userMgr.AddToRoleAsync(admin, SD.Admin).GetAwaiter().GetResult();
    }
}
}