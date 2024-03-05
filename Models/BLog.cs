using System.ComponentModel.DataAnnotations;

namespace Blog_1.Models
{
	public class BLog
	{
		public int Id { get; set; }

		[MaxLength(150)]
		public string Name { get; set; } = "";
		public string Description { get; set; } = "";
		public string Category { get; set; } = "";
		public string ImageFileName { get; set; } = "";
		[MaxLength(150)]
		public string Author { get; set; } = "";
		public DateTime CreatedAt { get; set; }
	}
}
