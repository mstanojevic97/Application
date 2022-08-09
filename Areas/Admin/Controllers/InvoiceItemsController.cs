using Application.Infrastructure;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class InvoiceItemsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public InvoiceItemsController(DataContext context, IWebHostEnvironment webHostEnvironment, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 10;
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)_context.InvoiceItems.Count() / pageSize);
            ViewBag.Users = _userManager.Users.ToList();
            var invoicess = _context.InvoiceItems;
            return View(await _context.InvoiceItems
                .Include(i => i.Invoice)
                .Include(i => i.Product)
                .Include(i => i.Size)
                .Skip((p - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync());
        }
        public async Task<IActionResult> Complete(int id)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(i => i.Id == id);
            invoice.Status = "Completed";
            _context.Update(invoice);
            await _context.SaveChangesAsync();
            TempData["Success"] = "The invoice has been completed!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            InvoiceItem invoiceItem = await _context.InvoiceItems.FindAsync(id);

            _context.InvoiceItems.Remove(invoiceItem);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Invoice has been deleted!";
            return RedirectToAction("Index");
        }
    }
}
