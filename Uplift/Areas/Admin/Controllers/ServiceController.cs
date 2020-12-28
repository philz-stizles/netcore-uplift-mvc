using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Repository;
using Uplift.Models;
using Uplift.Models.ViewModels;

namespace Uplift.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private ServiceViewModel ServiceVM;
        private readonly IWebHostEnvironment _env;
        public ServiceController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _env = env;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ServiceVM = new ServiceViewModel
            {
                Categories = _unitOfWork.Category.GetCategoryDropdown(),
                Frequencies = _unitOfWork.Frequency.GetFrequencyDropdown()
            };

            if (id == null) return View(ServiceVM);

            var existingService = await _unitOfWork.Service.Get(id.GetValueOrDefault());

            if (existingService == null) return NotFound();

            ServiceVM.Service = existingService;

            return View(ServiceVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Service service)
        {
            if (!ModelState.IsValid)
            {
                ServiceVM = new ServiceViewModel
                {
                    Categories = _unitOfWork.Category.GetCategoryDropdown(),
                    Frequencies = _unitOfWork.Frequency.GetFrequencyDropdown(),
                    Service = service
                };

                return View(ServiceVM);
            }

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var files = HttpContext.Request.Form.Files;

            if (service.Id == 0)
            {
                if(files != null && files.Count > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files[0].FileName);
                    var fileFullPath = Path.Combine(_env.WebRootPath, "images", "services", $"{guid}{extension}");
                    
                    using(var fileStream = new FileStream(fileFullPath, FileMode.Create)){
                        await files[0].CopyToAsync(fileStream);
                    }

                    service.ImageUrl = @"\images\services\" + guid + extension;
                }
                service.CreatedAt = DateTime.Now;
                service.CreatedBy = userId;
                await _unitOfWork.Service.Add(service);
            }
            else
            {
                var existingService = await _unitOfWork.Service.Get(service.Id);
                if(files != null && files.Count > 0)
                {
                    var existingImageLocation = Path.Combine(_env.WebRootPath, existingService.ImageUrl.TrimStart('\\'));
                    if(System.IO.File.Exists(existingImageLocation))
                    {
                        System.IO.File.Delete(existingImageLocation);
                    }

                    var guid = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files[0].FileName);
                    var fileFullPath = Path.Combine(_env.WebRootPath, "images", "services", $"{guid}{extension}");
                    using(var fileStream = new FileStream(fileFullPath, FileMode.Create)){
                        await files[0].CopyToAsync(fileStream);
                    }
                    service.ImageUrl = @"\images\services\" + guid + extension;
                }
                else
                {
                    service.ImageUrl = existingService.ImageUrl;
                }

                service.ModifiedAt = DateTime.Now;
                service.ModifiedBy = userId;
                await _unitOfWork.Service.Update(service);
            }

            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }


        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { 
                data = await _unitOfWork.Service.GetAll(includes: "Category,Frequency", orderBy: s => s.OrderByDescending(os => os.CreatedAt)) 
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var existingService = await _unitOfWork.Service.Get(id);
            if (existingService == null) return Json(new { status = false, message = "Delete failed" });

            _unitOfWork.Service.Remove(existingService);
            await _unitOfWork.SaveAsync();

            return Json(new { status = true, message = "Deleted successfully" });
        }

        #endregion
    }
}