using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MastersOfCinema.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public int DirectorId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Year { get; set; }
        public string Description { get; set; }
        public string MoviePosterName { get; set; }
    }
}
