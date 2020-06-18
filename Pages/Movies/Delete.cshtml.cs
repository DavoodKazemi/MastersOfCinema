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
    public class DeleteModel : PageModel
    {
        private readonly MastersOfCinema.Data.Context _context;

        public DeleteModel(MastersOfCinema.Data.Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; }
        [BindProperty]
        public Director Director { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Movie = await _context.Movie.FirstOrDefaultAsync(m => m.MovieId == id);
            Director = await _context.Director.FirstOrDefaultAsync(m => m.DirectorId == Movie.DirectorId);
            if (Movie == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie.FindAsync(id);

            if (Movie != null)
            {
                _context.Movie.Remove(Movie);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
