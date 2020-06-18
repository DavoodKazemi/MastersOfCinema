using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MastersOfCinema.Data;
using MastersOfCinema.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MastersOfCinema.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly Context _context;
        private readonly IWebHostEnvironment hostingEnvironment;

        public EditModel(Context context, IWebHostEnvironment environment)
        {
            _context = context;
            this.hostingEnvironment = environment;
        }

        [BindProperty]
        public Movie Movie { get; set; }
        [BindProperty]
        public IFormFile Image { set; get; }
        [BindProperty]
        public string OriginalImage { set; get; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie.FirstOrDefaultAsync(m => m.MovieId == id);
            OriginalImage = Movie.MoviePosterName;

            if (Movie == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (this.Image != null)
            {
                var fileName = GetUniqueName(this.Image.FileName);
                var posters = Path.Combine(hostingEnvironment.WebRootPath, "uploads/posters");
                var filePath = Path.Combine(posters, fileName);
                this.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                this.Movie.MoviePosterName = fileName; // Set the file name
            }
            else
            {
                Movie.MoviePosterName = OriginalImage;

            }
            _context.Attach(Movie).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.MovieId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            TempData["Message"] = "Movie saved!";
            return RedirectToPage("./Details", new { Id = Movie.MovieId });
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.MovieId == id);
        }
        private string GetUniqueName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_" + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }
    }
}
