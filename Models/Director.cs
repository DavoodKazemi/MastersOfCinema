using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MastersOfCinema.Models
{
    public class Director
    {
        public Director()
        {
            Movies = new List<Movie>();
        }
        public int DirectorId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Country { get; set; }
        public string Bio { get; set; }
        public List<Movie> Movies { get; set; }
        public string PhotoURL { get; set; }
    }
}
