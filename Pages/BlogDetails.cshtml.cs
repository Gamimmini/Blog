using Blog_1.Models;
using Blog_1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog_1.Pages
{
    public class BlogDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BLog Blog { get; set; } = new BLog();
        public BLog Category { get; set; } = new BLog();
        public List<BLog> RelatedBlogs { get; set; } = new List<BLog>();
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

            RelatedBlogs = _context.Blog
                    .Where(b => b.Category == blog.Category && b.Id != blog.Id)
                    .OrderBy(x => Guid.NewGuid())
                    .Take(5)
                    .ToList();
        }

    }
}
