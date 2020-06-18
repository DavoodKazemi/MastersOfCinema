using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MastersOfCinema.Data;
using MastersOfCinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MastersOfCinema.Pages
{
    public class SearchModel : PageModel
    {
        private readonly Context _context;

        public SearchModel(Context context)
        {
            this._context = context;
        }

        public IEnumerable<Director> Director { get; set; }
        public IEnumerable<Movie> Movie { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public async Task OnGetAsync(string searchTerm)
        {
            Director = await _context.Director.ToListAsync();
            Movie = await _context.Movie.ToListAsync();
            Director = GetDirectorByName(SearchTerm);
            Movie = GetMovie(SearchTerm);
            //SearchTerm = searchTerm;
            ViewData["SearchTerm"] = SearchTerm;
        }

        public IEnumerable<Director> GetDirectorByName(string searchString)
        {
            var query = from r in _context.Director
                        where r.Name.Contains(searchString) || r.Country.Contains(searchString) ||
                        string.IsNullOrEmpty(searchString)
                        orderby r.Name
                        select r;

            if (query.Any()) { searchTermFound = true; }

            return query;
        }
        public IEnumerable<Movie> GetMovie(string searchString)
        {
            var query = from r in _context.Movie
                        where r.Title.Contains(searchString) || r.Year.Contains(searchString) ||
                        string.IsNullOrEmpty(searchString)
                        orderby r.Title
                        select r;

            if (query.Any()) { searchTermFound = true; }

            return query;
        }

        public bool searchTermFound = false;

    }
}