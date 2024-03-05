using Blog_1.Models;
using Blog_1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace Blog_1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public List<BLog> Blog { get; set; } = new List<BLog>();
        public List<BLog> RandomBlog { get; set; } = new List<BLog>();
        public List<BLog> OldestBlogs { get; set; } = new List<BLog>();
        public List<BLog> CategoryNumber { get; set; } = new List<BLog>();


        public BLog CategoryDetails { get; set; } = new BLog();

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task OnGetAsync()
        {
            int pageSize = 5;

            var totalItems = await context.Blog.CountAsync();

            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            Blog = await context.Blog
                .OrderByDescending(p => p.Id)
                .Skip((PageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            RandomBlog = await context.Blog
                .OrderBy(x => Guid.NewGuid())
                .Take(3)
                .ToListAsync();

            OldestBlogs = await context.Blog
               .OrderBy(b => b.CreatedAt)
               .Take(3)
               .ToListAsync();

            CategoryNumber = await context.Blog.ToListAsync();

            CategoryDetails = new BLog();
        }
    }
}
