using Blog_1.Models;
using Blog_1.Services;


namespace Blog_1.Pages.Admin.Blog
{
    public class IndexModel : BasePageModel  
    {

        public IndexModel(ApplicationDbContext context) : base(context)
        {

        }

        public async Task OnGetAsync()
        {

            var query = context.Blog.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(b => b.Name.Contains(SearchTerm));
            }

            await LoadCommonDataAsync(query);  

        }
    }
}
