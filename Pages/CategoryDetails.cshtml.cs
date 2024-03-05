using Blog_1.Models;
using Blog_1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace Blog_1.Pages
{
    public class CategoryDetailsModel : PageModel
    {
        private readonly ApplicationDbContext context;
        public List<BLog> Blog { get; set; } = new List<BLog>();

        public List<BLog> OldestBlogs { get; set; } = new List<BLog>();
        public List<BLog> CategoryNumber { get; set; } = new List<BLog>();

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages { get; set; }

        public CategoryDetailsModel(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task OnGetAsync(string? Category)
        {
            int pageSize = 5;

            // Filter by Category if it's not null or empty
            var query = context.Blog.AsQueryable();
            if (!string.IsNullOrEmpty(Category))
            {
                query = query.Where(p => p.Category == Category);
            }

            var totalItems = await query.CountAsync();

            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            Blog = await query
                .OrderByDescending(p => p.Id)
                .Skip((PageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            OldestBlogs = await context.Blog
              .OrderBy(b => b.CreatedAt)
              .Take(3)
              .ToListAsync();

            CategoryNumber = await context.Blog.ToListAsync();
        }
    }
}
