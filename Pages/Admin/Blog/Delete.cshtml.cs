using Blog_1.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog_1.Pages.Admin.Blog
{
    public class DeleteModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        public DeleteModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }

        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Blog/Index");
                return;
            }
            var blog = context.Blog.Find(id);
            if (blog == null)
            {
                Response.Redirect("/Admin/Blog/Index");
                return;
            }

            string imageFullPath = environment.WebRootPath + "/image/" + blog.ImageFileName;
            System.IO.File.Delete(imageFullPath);

            context.Blog.Remove(blog);
            context.SaveChanges();

            Response.Redirect("/Admin/Blog/Index");
        }
    }
}
