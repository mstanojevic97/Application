using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Infrastructure;
using Application.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            int pageSize = 6;
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

            var productsByGender = _context.Products.Where(p => p.GenderId == gender.Id);
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)productsByGender.Count() / pageSize);

            return View(await productsByGender.OrderByDescending(p => p.Id).Skip((p - 1) * pageSize).Take(pageSize).ToListAsync());
        }
        public async Task<IActionResult> SingleProduct(int id)
        {
            Product product = await _context.Products.FindAsync(id);
            ViewBag.Inventories = _context.Inventories.Where(i => i.ProductId == id).ToList();
            var sizes = _context.Sizes.ToList();
            List<Size> sizes2 = new List<Size>();
            foreach(var size in sizes)
            {
                foreach(var inventory in ViewBag.Inventories)
                {
                    if(size.Id == inventory.SizeId)
                    {
                        sizes2.Add(size);
                    }
                }
            }
            ViewBag.Sizes = sizes2;
            return View(product);
        }
    }
}
