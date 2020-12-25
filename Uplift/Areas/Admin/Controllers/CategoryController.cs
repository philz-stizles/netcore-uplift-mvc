using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Repository;
using Uplift.Models;

namespace Uplift.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController: Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index() {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id) {
            if(id == null) return View();

            var existingCategory = await _unitOfWork.Category.Get(id.GetValueOrDefault());

            if(existingCategory == null) return NotFound();

            return View(existingCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Category category) {
            if(!ModelState.IsValid) return View(category);

            if(category.Id == 0)
            {
                await _unitOfWork.Category.Add(category);
            }
            else
            {
                await _unitOfWork.Category.Update(category);
            }

            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            return Json(new { data = await _unitOfWork.Category.GetAll() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            var existingCategory = await _unitOfWork.Category.Get(id);
            if(existingCategory == null) return Json(new { status = false, message = "Delete failed" });

            _unitOfWork.Category.Remove(existingCategory);
            await _unitOfWork.SaveAsync();

            return Json(new { status = true, message = "Deleted successfully" });
        }

        #endregion
    }
}