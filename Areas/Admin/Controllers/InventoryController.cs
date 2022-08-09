using Application.Models;
using Application.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class InventoryController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public InventoryController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 10;
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)_context.Inventories.Count() / pageSize);
            return View( await _context.Inventories.OrderByDescending(i => i.Id)
                .Include(i =>i.Product)
                .Include(i =>i.Size)
                .Skip((p - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync());
        }
        public IActionResult Add()
        {
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            ViewBag.Sizes = new SelectList(_context.Sizes, "Id", "Value");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Inventory inventory)
        {
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            ViewBag.Sizes = new SelectList(_context.Sizes, "Id", "Value");

            if (ModelState.IsValid)
            {
                var inventorySize = inventory.SizeId;
                var productSize = _context.Inventories.Where(i =>i.ProductId == inventory.ProductId && i.SizeId== inventory.SizeId);
                if (productSize.Count()!=0)
                {
                    ModelState.AddModelError(",", "Inventory already exists!");
                    return Redirect("Index");
                }
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Item has been added to Inventory!";
                return RedirectToAction("Index");
            }
            return View(inventory);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Inventory inventory = await _context.Inventories.FindAsync(id);
            ViewBag.Sizes = new SelectList(_context.Sizes, "Id", "Value");
            return View(inventory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Inventory inventory)
        {
            var inventorySize = inventory.SizeId;
            var productSize = _context.Inventories.Where(i => i.ProductId == inventory.ProductId && i.SizeId == inventory.SizeId);
            if (productSize.Count() != 0)
            {
                ModelState.AddModelError(",", "Inventory already exists!");
                return Redirect("Index");
            }
            _context.Update(inventory);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Item in inventory has been updated!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Inventory inventory = await _context.Inventories.FindAsync(id);

            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Item has been deleted!";
            return RedirectToAction("Index");
        }
    }
}