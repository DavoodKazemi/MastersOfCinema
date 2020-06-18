using MastersOfCinema.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MastersOfCinema.ViewComponents
{
    public class DirectorMovieCountViewComponent
        :ViewComponent
    {
        private readonly Context _context;

        public int GetCountOfDirectors()
        {
            return _context.Director.Count();
        }

        public DirectorMovieCountViewComponent(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var directorCount = GetCountOfDirectors();
            return View(directorCount);
        }
    }
}
