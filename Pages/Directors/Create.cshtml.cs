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

namespace MastersOfCinema.Pages.Directors
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

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Director Director { get; set; }
        [BindProperty]
        public IFormFile Image { set; get; }

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
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads, fileName);
                this.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                this.Director.PhotoURL = fileName; // Set the file name
            }
            else
            {
                this.Director.PhotoURL = "DirectorsDefaultImage.jpg";
            }

            _context.Director.Add(Director);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
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
