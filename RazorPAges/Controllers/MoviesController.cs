using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorPAges.Models;

namespace RazorPAges.Controllers
{
    [Produces("application/json")]
    [Route("api/Movie")]
    public class MovieController : Controller
    {
        private readonly MovieContext _context;
        public MovieController(MovieContext context)
        {
            _context = context;
            if (_context.Movie.Count() == 0)
            {
                _context.Movie.Add(new Movie { Title = "Movie1", Genre = "Horror1", Price = 121, ReleaseDate = new DateTime().AddDays(1) });
                _context.Movie.Add(new Movie { Title = "Movie2", Genre = "Horror2", Price = 122, ReleaseDate = new DateTime().AddDays(2) });
                _context.Movie.Add(new Movie { Title = "Movie3", Genre = "Horror3", Price = 123, ReleaseDate = new DateTime().AddDays(3) });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Movie> GetAll()
        {
            return _context.Movie.ToList();
        }

        [HttpGet("{id}", Name = "GetMovie")]
        public IActionResult GetById(long id)
        {
            var movie = _context.Movie.FirstOrDefault(t => t.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return new ObjectResult(movie);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Movie movie)
        {
            if (movie == null)
            {
                return BadRequest();
            }

            _context.Movie.Add(movie);
            _context.SaveChanges();

            return CreatedAtRoute("GetMovie", new { id = movie.ID }, movie);
        }


        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Movie mov)
        {
            if (mov == null || mov.ID != id)
            {
                return BadRequest();
            }

            var movie = _context.Movie.FirstOrDefault(t => t.ID == id);
            if (mov == null)
            {
                return NotFound();
            }
            movie.Title = mov.Title;
            movie.ReleaseDate = mov.ReleaseDate;
            movie.Genre = mov.Genre;
            movie.Price = mov.Price;
            
            _context.Movie.Update(movie);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {

            var movie = _context.Movie.FirstOrDefault(t => t.ID == id);
            if (movie == null)
            {
                return NotFound();
            }
            _context.Movie.Remove(movie);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}