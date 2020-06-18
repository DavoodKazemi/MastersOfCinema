using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MastersOfCinema.Data;
using MastersOfCinema.Models;

namespace MastersOfCinema.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly MastersOfCinema.Data.Context _context;

        public IndexModel(MastersOfCinema.Data.Context context)
        {
            _context = context;
        }
        public IList<Movie> Movie { get;set; }
        public IList<Director> DirectorList { get; set; }
        public int MovieCount { get; set; }
        public async Task OnGetAsync()
        {
            Movie = await _context.Movie.ToListAsync();
            DirectorList = await _context.Director.ToListAsync();
            MovieCount = _context.Movie.Count();

        }
    }
}
