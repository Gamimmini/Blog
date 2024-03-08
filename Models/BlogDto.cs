using System.ComponentModel.DataAnnotations;

namespace Blog_1.Models
{
    public class BlogDto
    {
        [Required, MaxLength(150)]
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; } 
        public IFormFile? ImageFile { get; set; }
        [Required, MaxLength(150)]
        public string Author { get; set; } = "";
    }
}
