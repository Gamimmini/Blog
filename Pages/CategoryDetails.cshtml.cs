using Blog_1.Services;
namespace Blog_1.Pages
{
    public class CategoryDetailsModel : BasePageModel
    {
        public CategoryDetailsModel(ApplicationDbContext context) : base(context)
        {
        }

        public async Task OnGetAsync(string? Category)
        {
            int pageSize = 5;
            var query = context.Blog.AsQueryable();

            if (!string.IsNullOrEmpty(Category))
            {
                query = query.Where(p => p.Category == Category);
                SearchTerm = Category;
            }

            await LoadCommonDataAsync(query, pageSize);
        }
    }
}