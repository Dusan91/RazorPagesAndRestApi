using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPAges.Models;

namespace RazorPAges.PagesMovies
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPAges.Models.MovieContext _context;

        public DeleteModel(RazorPAges.Models.MovieContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie.SingleOrDefaultAsync(m => m.ID == id);

            if (Movie == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            string url = "http://localhost:60711/api/Movie";

            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie.FindAsync(id);

            if (Movie != null)
            {
                //_context.Movie.Remove(Movie);
                //await _context.SaveChangesAsync();
                using (HttpClient client = new HttpClient())
                {
                    //StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(Movie), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.DeleteAsync(string.Format(url+"/"+Movie.ID));
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
