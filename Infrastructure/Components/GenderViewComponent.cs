using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Components
{
    public class GenderViewComponent:ViewComponent
    {
        private readonly DataContext _context;
        public GenderViewComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync() => View(await _context.Genders.ToListAsync());
    }
}
