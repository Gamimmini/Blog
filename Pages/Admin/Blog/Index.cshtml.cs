using Blog_1.Models;
using Blog_1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Blog_1.Pages.Admin.Blog
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public List<BLog> Blog { get; set; } = new List<BLog>();
        public int PageSize { get; set; } = 5;
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
            SearchTerm = "";
        }

        public async Task OnGetAsync(int? pageNumber)
        {
            PageNumber = pageNumber ?? 1;

            var query = context.Blog.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(b => b.Name.Contains(SearchTerm));
            }

            var totalItems = await query.CountAsync();

            TotalPages = (int)Math.Ceiling((double)totalItems / PageSize);

            Blog = await query
                .OrderByDescending(p => p.Id)
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }

    }
}
