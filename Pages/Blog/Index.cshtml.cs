using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Models;
using Microsoft.AspNetCore.Razor.Language;

namespace EntityFramework.Pages_Blog
{
    public class IndexModel : PageModel
    {
        private readonly EntityFramework.Models.MyBlogContext _context;

        public IndexModel(EntityFramework.Models.MyBlogContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get; set; } = default!;

        public const int ITEM_PER_PAGE = 10;
        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentpage { get; set; }
        public int countPages { get; set; }

        public async Task OnGetAsync()
        {
            // Article = await _context.articles.ToListAsync();
            int totalArticle = await _context.articles.CountAsync();
            countPages = (int)Math.Ceiling((double)totalArticle / ITEM_PER_PAGE);
            if (currentpage < 1)
                currentpage = 1;
            if (currentpage > countPages)
                currentpage = countPages;
            var qr = (from a in _context.articles
                      orderby a.Created descending
                      select a).Skip((currentpage - 1) * ITEM_PER_PAGE).Take(ITEM_PER_PAGE).ToList();
            Article = qr;
        }
    }
}
