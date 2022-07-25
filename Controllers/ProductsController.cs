using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Infrastructure;
using Application.Models;

namespace Application.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataContext _context;
        public ProductsController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string genderSlug = "", int p = 1)
        {
            Console.WriteLine(await _context.Genders.FirstOrDefaultAsync());
            int pageSize = 5;
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.GenreSlug = genderSlug;
            if (genderSlug == "")
            {
                ViewBag.TotalPages = (int)Math.Ceiling((decimal)_context.Products.Count() / pageSize);
                return View(await _context.Products.OrderByDescending(p => p.Id).Skip((p - 1) * pageSize).Take(pageSize).ToListAsync());
            }
            Gender gender = await _context.Genders.Where(g => g.Slug == genderSlug).FirstOrDefaultAsync();
            if (gender == null) return RedirectToAction("Index"); ;

            var productsByGender = _context.Products.Where(p => p.CategoryId == gender.Id);
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)productsByGender.Count() / pageSize);

            return View(await productsByGender.OrderByDescending(p => p.Id).Skip((p - 1) * pageSize).Take(pageSize).ToListAsync());
        }
    }
}
