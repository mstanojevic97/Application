using Application.Infrastructure;
using Application.Models;
using Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Application.Controllers
{
    
    public class CartController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        public CartController(DataContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart")?? new List<CartItem>();
            CartViewModel cartVM = new()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };
            return View(cartVM);
        }
        [HttpPost]
        public async Task<IActionResult> Add(int id, int id2)
        {
            Product product = await _context.Products.FindAsync(id);
            Size size = await _context.Sizes.FindAsync(id2);
            Inventory inventory = _context.Inventories.Where(i => i.ProductId == id && i.SizeId == id2).FirstOrDefault();
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();
            if(cartItem == null || cartItem.SizeId!=id2)
            {
                CartItem cartItem1 = new CartItem(product);
                cartItem1.SizeId = id2;
                cartItem1.Size = size;
                cartItem1.Amount = inventory.Amount;
                cart.Add(cartItem1);
                TempData["Success"] = "The product has been added!";
            }
            else
            {
                if(cartItem.Quantity<cartItem.Amount)
                {
                    cartItem.Quantity++;
                    TempData["Success"] = "The product has been added!";
                }
                else
                {
                    TempData["Error"] = "No more product left in store!";
                }
            }

            HttpContext.Session.SetJson("Cart", cart);
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();
            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == id);
            }
            if(cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            
            TempData["Success"] = "The product has been removed!";

            return RedirectToAction("Index");
        }
        public IActionResult Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            cart.RemoveAll(p => p.ProductId == id);
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
                return RedirectToAction("Index");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The product has been removed!";

            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Finish(List<CartItem> cart)
        {
            var items = cart;
            var user = await _userManager.GetUserAsync(User);
            var invoice = new Invoice();
            if(user!=null)
            {
                invoice.UserId = user.Id;
            }
            else
            {
                invoice.UserId = null;
            }
            invoice.Created = DateTime.Now;
            invoice.Status = "In Progres";
            _context.Add(invoice);
            _context.SaveChanges();
            foreach (var i in items)
            {
                var invoiceItem = new InvoiceItem();
                invoiceItem.InvoiceId = invoice.Id;
                invoiceItem.ProductId = i.ProductId;
                invoiceItem.SizeId = i.SizeId;
                invoiceItem.Amount = i.Quantity;
                var inventory = _context.Inventories.Where(ii => ii.ProductId == i.ProductId && ii.SizeId == i.SizeId).FirstOrDefault();
                inventory.Amount -= i.Quantity;
                _context.Update(inventory);
                _context.Add(invoiceItem);
                _context.SaveChanges();
            }
            TempData["Success"] = "Shopping completed!";

            return Redirect("Index");
        }
    }
}
