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
    public class IndexModel : PageModel
    {
        private readonly MastersOfCinema.Data.Context _context;

        public IndexModel(MastersOfCinema.Data.Context context)
        {
            _context = context;
        }

        public List<Director> Director { get; set; }
       

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public async Task OnGetAsync()
        {

            Director = await _context.Director.ToListAsync();
        }


        

        
    }
}
