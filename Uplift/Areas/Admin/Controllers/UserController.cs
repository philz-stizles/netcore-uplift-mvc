using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Repository;
using Uplift.Utility;

namespace Uplift.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Admin)]
    public class UserController: Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index() 
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            return View(await _unitOfWork.User.GetAll(filter: u => u.Id != userId));
        }

        public async Task<IActionResult> Lock(string userId) 
        {
            if(userId == null) return NotFound();

            await _unitOfWork.User.LockUser(userId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UnLock(string userId)
        {
            if (userId == null) return NotFound();

            await _unitOfWork.User.UnLockUser(userId);

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        #endregion
    }
}