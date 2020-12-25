using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Uplift.DataAccess.Repository;
using Uplift.Models.ViewModels;
using Uplift.Utility;

namespace Uplift.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private HomeViewModel HomeVM;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM = new HomeViewModel() {
                Categories = await _unitOfWork.Category.GetAll(),
                Services = await _unitOfWork.Service.GetAll(includes: "Frequency")
            };

            return View(HomeVM);
        }

        public async Task<IActionResult> Details(int id)
        {
            var existingService = await _unitOfWork.Service.Get(filter: s => s.Id == id, includes: "Frequency");

            if(existingService == null) return NotFound();

            return View(existingService);
        }

        public async Task<IActionResult> AddToCart(int serviceId)
        {
            var existingService = await _unitOfWork.Service.Get(filter: s => s.Id == serviceId, includes: "Frequency");

            if(existingService == null) return NotFound();

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SD.SessionCart)))
            {
                var sessionList = new List<int>
                {
                    existingService.Id
                };
                HttpContext.Session.SetString(SD.SessionCart, JsonConvert.SerializeObject(sessionList));
            }
            else {
                var sessionList = JsonConvert.DeserializeObject<List<int>>(HttpContext.Session.GetString(SD.SessionCart));
                if (!sessionList.Contains(serviceId))
                {
                    sessionList.Add(serviceId);
                    HttpContext.Session.SetString(SD.SessionCart, JsonConvert.SerializeObject(sessionList));
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}