using Blog_1.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog_1.Services
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
			
		}
		public DbSet<BLog> Blog { get; set; }
	}
}
