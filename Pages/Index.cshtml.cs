using Blog_1.Models;
using Blog_1.Services;
using Microsoft.EntityFrameworkCore;
namespace Blog_1.Pages
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(ApplicationDbContext context) : base(context)
        {
        }

        public async Task OnGetAsync()
        {
            int pageSize = 5;
            var query = context.Blog.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(b => b.Name.Contains(SearchTerm));
            }

            await LoadCommonDataAsync(query, pageSize);

            RandomBlog = await context.Blog
                .OrderBy(x => Guid.NewGuid())
                .Take(3)
                .ToListAsync();

            CategoryDetails = new BLog();
        }
    }
}