using Blog_1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace Blog_1.Models
{
    public class BasePageModel : PageModel
    {
        protected readonly ApplicationDbContext context;

        public List<BLog> Blog { get; set; } = new List<BLog>();
        public List<BLog> RandomBlog { get; set; } = new List<BLog>();
        public List<BLog> OldestBlogs { get; set; } = new List<BLog>();
        public List<BLog> CategoryNumber { get; set; } = new List<BLog>();
        public BLog CategoryDetails { get; set; } = new BLog();

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        readonly int pageSize = 5;
        public BasePageModel(ApplicationDbContext context)
        {
            this.context = context;
            SearchTerm = "";
        }

        public async Task LoadCommonDataAsync(IQueryable<BLog> query)
        {
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
