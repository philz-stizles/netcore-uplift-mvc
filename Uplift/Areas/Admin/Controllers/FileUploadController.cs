using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uplift.DataAccess;
using Uplift.Models;

namespace Uplift.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class FileUploadController : Controller
    {
        private readonly ApplicationDbContext _db;

        public FileUploadController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index() 
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id) {
            if(id == null) return View();

            var existingFileUpload = await _db.FileUploads.FindAsync(id.GetValueOrDefault());

            if(existingFileUpload == null) return NotFound();

            return View(existingFileUpload);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(int id, FileUpload fileUpload) 
        {
            if(!ModelState.IsValid) return View(fileUpload);

            byte[] fileData = null;
            var files = HttpContext.Request.Form.Files;

            if(files != null && files.Count > 0)
            {
                using(var fileStream = files[0].OpenReadStream())
                using(var memoryStream = new MemoryStream())
                {
                    await fileStream.CopyToAsync(memoryStream);
                    fileData = memoryStream.ToArray();
                }

                fileUpload.FileData = fileData;
            }

            if(fileUpload.Id == 0)
            {
                await _db.FileUploads.AddAsync(fileUpload);
            }
            else
            {
                var existingFileUpload = await _db.FileUploads.FindAsync(id);
                existingFileUpload.Name = fileUpload.Name;
                if(files != null && files.Count > 0)
                {
                    existingFileUpload.FileData = fileData;
                }
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            return Json(new { data = await _db.FileUploads.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            var existingFileUpload = await _db.FileUploads.FindAsync(id);
            if(existingFileUpload == null) return Json(new { status = false, message = "Delete failed" });

            _db.FileUploads.Remove(existingFileUpload);
            await _db.SaveChangesAsync();

            return Json(new { status = true, message = "Deleted successfully" });
        }
        #endregion
    }
}