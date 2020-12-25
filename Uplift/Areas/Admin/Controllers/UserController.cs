using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Repository;
using Uplift.Models;

namespace Uplift.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController: Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index() {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id) {
            if(id == null) return View();

            var existingUser = await _unitOfWork.User.Get(id.GetValueOrDefault());

            if(existingUser == null) return NotFound();

            return View(existingUser);
        }

        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Upsert(User User) {
        //     if(!ModelState.IsValid) return View(User);

        //     if(string.IsNullOrEmpty(User.Id))
        //     {
        //         await _unitOfWork.User.Add(User);
        //     }
        //     else
        //     {
        //         await _unitOfWork.User.Update(User);
        //     }

        //     await _unitOfWork.SaveAsync();

        //     return RedirectToAction(nameof(Index));
        // }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            return Json(new { data = await _unitOfWork.User.GetAll() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            var existingUser = await _unitOfWork.User.Get(id);
            if(existingUser == null) return Json(new { status = false, message = "Delete failed" });

            _unitOfWork.User.Remove(existingUser);
            await _unitOfWork.SaveAsync();

            return Json(new { status = true, message = "Deleted successfully" });
        }

        #endregion
    }
}