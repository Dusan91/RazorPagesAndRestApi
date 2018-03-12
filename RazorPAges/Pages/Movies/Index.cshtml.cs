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
    public class IndexModel : PageModel
    {
        private readonly RazorPAges.Models.MovieContext _context;

        public IndexModel(RazorPAges.Models.MovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }

        public async Task OnGetAsync()
        {
            //Movie = await _context.Movie.ToListAsync();
            string url = "http://localhost:60711/api/Movie";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(string.Format(url));
            string json = await response.Content.ReadAsStringAsync();
            IList<Movie> value = JsonConvert.DeserializeObject<IList<Movie>>(json);
            Movie = value;
        }
    }
}
