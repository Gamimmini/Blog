using Blog_1.Models;
using Blog_1.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
namespace Blog_1.Pages
{
    public class BlogDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BLog Blog { get; set; } = new BLog();
        public BLog Category { get; set; } = new BLog();
        public BlogDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Index");
                return;
            }
            var blog = _context.Blog.Find(id);
            if (blog == null)
            {
                Response.Redirect("/Index");
                return;
            }
            Blog = blog;

        
        }
    }
}
