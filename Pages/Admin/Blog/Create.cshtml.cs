using Blog_1.Models;
using Blog_1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog_1.Pages.Admin.Blog
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public BlogDto BlogDto { get; set; } = new BlogDto();

        public CreateModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }

        public void OnGet()
        {

        }

        public string errorMessage = "";
        public string successMessage = "";
        public void OnPost()
        {
            if (BlogDto.ImageFile == null)
            {
                ModelState.AddModelError("BlogDto.ImageFile", "The image File is required");
            }
            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields";
                return;
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(BlogDto.ImageFile!.FileName);
            string imageFullPath = environment.WebRootPath + "/image/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                BlogDto.ImageFile.CopyTo(stream);
            }
            BLog blog = new()
            {
                Name = BlogDto.Name,
                Description = BlogDto.Description ?? "",
                Category = BlogDto.Category ?? "",
                ImageFileName = newFileName,
                Author = BlogDto.Author,
                CreatedAt = DateTime.Now,
            };
            context.Blog.Add(blog);
            context.SaveChanges();

            BlogDto.Name = "";
            BlogDto.Description = "";
            BlogDto.Category = "";
            BlogDto.ImageFile = null;
            BlogDto.Author = "";
            ModelState.Clear();

            successMessage = "New Blog Created Succesfully";

            Response.Redirect("/Admin/Blog");
        }
    }
}
