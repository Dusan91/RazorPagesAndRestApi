using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RazorPAges.Models;

namespace RazorPAges.PagesMovies
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPAges.Models.MovieContext _context;

        public DetailsModel(RazorPAges.Models.MovieContext context)
        {
            _context = context;
        }

        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string url = "http://localhost:60711/api/Movie";

            if (id == null)
            {
                return NotFound();
            }

            //Movie = await _context.Movie.SingleOrDefaultAsync(m => m.ID == id);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(string.Format(url+"/"+id));
            string json = await response.Content.ReadAsStringAsync();
            Movie value = JsonConvert.DeserializeObject<Movie>(json);
            Movie = value;
            if (Movie == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
