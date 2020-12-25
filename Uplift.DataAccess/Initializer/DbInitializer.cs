using System;
using Microsoft.AspNetCore.Identity;
using Uplift.Models;

namespace Uplift.DataAccess.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userMgr;
        private readonly RoleManager<IdentityRole> _roleMgr;

        public DbInitializer(ApplicationDbContext context, UserManager<User> userMgr,
            RoleManager<IdentityRole> roleMgr)
        {
            _roleMgr = roleMgr;
            _userMgr = userMgr;
            _context = context;
        }

    public void Initialize()
    {
        try
        {

        }
        catch( Exception ex)
        {

        }
    }
}
}