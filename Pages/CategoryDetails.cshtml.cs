using Blog_1.Models;
using Blog_1.Services;
using Microsoft.AspNetCore.Mvc;
namespace Blog_1.Pages
{
    public class CategoryDetailsModel : BasePageModel
    {
        [BindProperty(SupportsGet = true)]
        public string TotalCategory { get; set; }
        public CategoryDetailsModel(ApplicationDbContext context) : base(context)
        {
            TotalCategory = "";
        }

        public async Task OnGetAsync(string? Category)
        {
            var query = context.Blog.AsQueryable();

            if (!string.IsNullOrEmpty(Category))
            {
                query = query.Where(p => p.Category == Category);
                TotalCategory = Category;
            }

            await LoadCommonDataAsync(query);
        }
    }
}