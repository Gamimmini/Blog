using Blog_1.Models;
using Blog_1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog_1.Pages.Admin.Blog
{
    public class IndexModel : PageModel
    {
		private readonly ApplicationDbContext context;

		public List<BLog> Blog { get; set; } = new List<BLog>();

        public IndexModel(ApplicationDbContext context)
		{
			this.context = context;

		}
		public void OnGet()
		{
            Blog = context.Blog.OrderByDescending(p => p.Id).ToList();
		}
	}
}
