using System;
using System.Collections.Generic;
using System.Linq;
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

        [BindProperty]
        private CartVM CartVM { get; set; }

        [BindProperty]
        private CartSummaryVM CartSummaryVM { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            CartVM = new CartVM()
            {
                Services = new List<Service>()
            };

            CartSummaryVM = new CartSummaryVM()
            {
                CustomerDetails = new Uplift.Models.Customer(),
                Services = new List<Service>()
            };
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
                    CartVM.Services.Add(service);
                }
            }

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
                    CartSummaryVM.Services.Add(service);
                }
            }

            return View(CartSummaryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(CartSummaryVM vm)
        {
            var listOfItems = new List<Service>();
            var sessionCart = HttpContext.Session.GetString(SD.SessionCart);
            if (!string.IsNullOrEmpty(sessionCart))
            {
                var cartItems = JsonConvert.DeserializeObject<List<int>>(sessionCart);

                vm.Services = new List<Service>();
                foreach (var id in cartItems)
                {
                    var service = await _unitOfWork.Service.Get(filter: s => s.Id == id, includes: "Frequency");
                    vm.Services.Add(service);
                }
            }

            if (!ModelState.IsValid) return View(vm);

            var newOrder = new Order
            {
                OrderReference = Guid.NewGuid().ToString().Substring(0, 7),
                Status = OrderStatus.Pending,
                CustomerDetail = vm.CustomerDetails,
                totalItems = vm.Services.Count,
                totalPrice = vm.Services.Sum(s => s.Price),
                OrderServices = new List<OrderService>(),
                OrderedAt = DateTime.Now
            };
            await _unitOfWork.Order.Add(newOrder);

            foreach (var item in vm.Services)
            {
                newOrder.OrderServices.Add(new OrderService
                {
                    Order = newOrder,
                    Service = item
                });
            }
            
            await _unitOfWork.SaveAsync();

            HttpContext.Session.SetString(SD.SessionCart, JsonConvert.SerializeObject(new List<int>()));

            return RedirectToAction("OrderConfirmation", "Cart", new { OrderReference = newOrder.Id});
        }

        public IActionResult OrderConfirmation(string orderReference)
        {
            return View(orderReference);
        }
    }
}