using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPAges.Models;

namespace RazorPAges.PagesMovies
{
    public class CreateModel : PageModel
    {
        private readonly RazorPAges.Models.MovieContext _context;

        public CreateModel(RazorPAges.Models.MovieContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            string url = "http://localhost:60711/api/Movie";
            if (!ModelState.IsValid)
            {
                return Page();
            }
            using (HttpClient client = new HttpClient()) {
                StringContent content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(Movie), Encoding.UTF8, "application/json");
                HttpResponseMessage response= await client.PostAsync(string.Format(url), content);
            }
            //_context.Movie.Add(Movie);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}