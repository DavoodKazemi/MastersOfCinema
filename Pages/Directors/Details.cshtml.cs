using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MastersOfCinema.Data;
using MastersOfCinema.Models;

namespace MastersOfCinema.Pages.Directors
{
    public class DetailsModel : PageModel
    {
        private readonly MastersOfCinema.Data.Context _context;

        public DetailsModel(MastersOfCinema.Data.Context context)
        {
            _context = context;
        }
        
        public Director Director { get; set; }
        [TempData]
        public string Message { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Director = await _context.Director.FirstOrDefaultAsync(m => m.DirectorId == id);


            //Test
            var directors = await _context.Director.Include(x => x.Movies).ToListAsync();

            if (Director == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
