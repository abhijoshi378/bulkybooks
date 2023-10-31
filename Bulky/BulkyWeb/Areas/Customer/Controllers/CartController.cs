using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(ILogger<CartController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM = new ShoppingCartVM()
            {
                shoppingCartList = _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == userId, includeProperties: "Product").ToList()
            };

            foreach(var c in ShoppingCartVM.shoppingCartList)
            {
                c.Price = GetQuantityBasedPrice(c);
                ShoppingCartVM.OrderTotal = ShoppingCartVM.OrderTotal + (c.Price * c.Count);
            }
            return View(ShoppingCartVM);
        }

        private double GetQuantityBasedPrice(ShoppingCart shoppingCart)
        {
            if (shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.Price;
            }
            else if (shoppingCart.Count <= 100)
            {
                return shoppingCart.Product.Price50;
            }
            else
            {
                return shoppingCart.Product.Price100;
            }
        }
    }
}