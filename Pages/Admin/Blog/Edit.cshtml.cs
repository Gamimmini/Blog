using Blog_1.Models;
using Blog_1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog_1.Pages.Admin.Blog
{
    public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public BlogDto BlogDto { get; set; } = new BlogDto();

        public BLog Blog { get; set; } = new BLog();
        public string errorMessage = "";
        public string successMessage = "";
        public EditModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }

        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Blog");
                return;
            }
            var blog = context.Blog.Find(id);
            if (blog == null)
            {
                Response.Redirect("/Admin/Blog");
                return;
            }

            BlogDto.Name = blog.Name;
            BlogDto.Description = blog.Description;
            BlogDto.Category = blog.Category.ToString();
            BlogDto.Author = blog.Author;
            Blog = blog;
        }

        public void OnPost(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Blog");
                return;
            }
            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields";
                return;
            }
            var blog = context.Blog.Find(id);
            if (blog == null)
            {
                Response.Redirect("/Admin/Blog");
                return;
            }

            string newFileName = blog.ImageFileName;
            if (BlogDto.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(BlogDto.ImageFile!.FileName);

                string imageFullPath = environment.WebRootPath + "/image/" + newFileName;

                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    BlogDto.ImageFile.CopyTo(stream);
                }

                string oldImageFullPath = environment.WebRootPath + "/image/" + blog.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
            }

            blog.Name = BlogDto.Name;
            blog.Description = BlogDto.Description ?? "";
            blog.Category = BlogDto.Category ?? "";
            blog.ImageFileName = newFileName;
            blog.Author = BlogDto.Author;

            context.SaveChanges();

            Blog = blog;

            successMessage = "Blog updated successfully";

            Response.Redirect("/Admin/Blog");


        }
    }
}
