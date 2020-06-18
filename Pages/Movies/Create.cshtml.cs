using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MastersOfCinema.Data;
using MastersOfCinema.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MastersOfCinema.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly Context _context;

        private readonly IWebHostEnvironment hostingEnvironment;

        public CreateModel(Context context, IWebHostEnvironment environment)
        {
            this.hostingEnvironment = environment;
            _context = context;
        }
        public string GetDirectorName(int Id)
        {
            Director2 = _context.Director.Find(Id);
            //string DirectorName = director.Name;
            return Director2.Name;
        }

        [BindProperty]
        public Movie Movie { get; set; }
        [BindProperty]
        public Director Director2 { get; set; }
        [BindProperty]
        public IFormFile Image { set; get; }
        public IActionResult OnGet(int directorId)
        {
            Movie = new Movie { DirectorId = directorId };
            
            return Page();
        }

        

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/
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
                this.Movie.MoviePosterName = "MoviesDefaultImage.jpg";
            }


            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Your new movie is added!";
            return RedirectToPage("./Details", new { Id = Movie.MovieId });
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
