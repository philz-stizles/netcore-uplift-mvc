using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Repository;
using Uplift.Models;

namespace Uplift.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController: Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index() {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id) {
            if(id == null) return View();

            var existingOrder = await _unitOfWork.Order.Get(id.GetValueOrDefault());

            if(existingOrder == null) return NotFound();

            return View(existingOrder);
        }

        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Upsert(Order Order) {
        //     if(!ModelState.IsValid) return View(Order);

        //     if(string.IsNullOrEmpty(Order.Id))
        //     {
        //         await _unitOfWork.Order.Add(Order);
        //     }
        //     else
        //     {
        //         await _unitOfWork.Order.Update(Order);
        //     }

        //     await _unitOfWork.SaveAsync();

        //     return RedirectToAction(nameof(Index));
        // }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            return Json(new { data = await _unitOfWork.Order.GetAll(includes: "CustomerDetail") });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPending()
        {
            return Json(new { data = await _unitOfWork.Order.GetAll() });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApproved()
        {
            return Json(new { data = await _unitOfWork.Order.GetAll(includes: "CustomerDetail") });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            var existingOrder = await _unitOfWork.Order.Get(id);
            if(existingOrder == null) return Json(new { status = false, message = "Delete failed" });

            _unitOfWork.Order.Remove(existingOrder);
            await _unitOfWork.SaveAsync();

            return Json(new { status = true, message = "Deleted successfully" });
        }

        #endregion
    }
}