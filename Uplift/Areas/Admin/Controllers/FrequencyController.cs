using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Repository;
using Uplift.Models;

namespace Uplift.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FrequencyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public FrequencyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id) {
            if(id == null) return View();

            var existingFrequency = await _unitOfWork.Frequency.Get(id.GetValueOrDefault());

            if(existingFrequency == null) return NotFound();

            return View(existingFrequency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Frequency frequency) {
            if(!ModelState.IsValid) return View(frequency);

            if(frequency.Id == 0)
            {
                await _unitOfWork.Frequency.Add(frequency);
            }
            else
            {
                await _unitOfWork.Frequency.Update(frequency);
            }

            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }


        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _unitOfWork.Frequency.GetAll() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var existingFrequency = await _unitOfWork.Frequency.Get(id);
            if (existingFrequency == null) return Json(new { status = false, message = "Delete failed" });

            _unitOfWork.Frequency.Remove(existingFrequency);
            await _unitOfWork.SaveAsync();

            return Json(new { status = true, message = "Deleted successfully" });
        }

        #endregion
    }
}