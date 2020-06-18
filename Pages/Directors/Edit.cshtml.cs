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

namespace MastersOfCinema.Pages.Directors
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
        public Director Director { get; set; }
        [BindProperty]
        public IFormFile Image { set; get; }
        [BindProperty]
        public string OriginalImage { set; get; }
        public async Task<IActionResult> OnGetAsync(int? directorId)
        {
            if (directorId == null)
            {
                return NotFound();
            }
            
            Director = await _context.Director.FirstOrDefaultAsync(m => m.DirectorId == directorId);
            OriginalImage = Director.PhotoURL;

            if (Director == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? directorId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            if (this.Image != null)
            {
                var fileName = GetUniqueName(this.Image.FileName);
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads, fileName);
                this.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                this.Director.PhotoURL = fileName; // Set the file name
            }
            else
            {
                Director.PhotoURL = OriginalImage;

            }
            _context.Attach(Director).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectorExists(Director.DirectorId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["Message"] = "Director saved!";
            return RedirectToPage("./Details", new { Id = Director.DirectorId });
        }

        private bool DirectorExists(int id)
        {
            return _context.Director.Any(e => e.DirectorId == id);
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
