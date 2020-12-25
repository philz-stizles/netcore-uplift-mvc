using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Uplift.DataAccess.Repository;
using Uplift.Models;
using Uplift.Models.ViewModels;
using Uplift.Utility;

namespace Uplift.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private CartViewModel CartVM;
        private CartSummaryViewModel CartSummaryVM;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var listOfItems = new List<Service>();
            var sessionCart = HttpContext.Session.GetString(SD.SessionCart);
            if (!string.IsNullOrEmpty(sessionCart)) 
            {
                var cartItems = JsonConvert.DeserializeObject<List<int>>(sessionCart);
                
                foreach (var id in cartItems)
                {
                    var service = await _unitOfWork.Service.Get(filter: s => s.Id == id, includes: "Frequency");
                    listOfItems.Add(service);
                }
            }

            CartVM = new CartViewModel()
            {
                Services = listOfItems
            };

            return View(CartVM);
        }
        

        public IActionResult RemoveFromCart(int cartItemId)
        {
            var sessionCart = HttpContext.Session.GetString(SD.SessionCart);
            var cartItems = JsonConvert.DeserializeObject<List<int>>(sessionCart);
            cartItems.Remove(cartItemId);
            HttpContext.Session.SetString(SD.SessionCart, JsonConvert.SerializeObject(cartItems));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Summary()
        {
            var listOfItems = new List<Service>();
            var sessionCart = HttpContext.Session.GetString(SD.SessionCart);
            if (!string.IsNullOrEmpty(sessionCart)) 
            {
                var cartItems = JsonConvert.DeserializeObject<List<int>>(sessionCart);
                
                foreach (var id in cartItems)
                {
                    var service = await _unitOfWork.Service.Get(filter: s => s.Id == id, includes: "Frequency");
                    listOfItems.Add(service);
                }
            }

            CartSummaryVM = new CartSummaryViewModel()
            {
                Services = listOfItems
            };

            return View(CartSummaryVM);
        }
    }
}