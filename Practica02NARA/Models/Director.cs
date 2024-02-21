using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Practica02NARA.Models
{
    public partial class Director
    {
        public Director()
        {
            Peliculas = new HashSet<Pelicula>();
        }

        public int IdDirector { get; set; }
        [Display(Name ="Nombre Director")]
        public string NombreDirector { get; set; } = null!;

        public virtual ICollection<Pelicula> Peliculas { get; set; }
    }
}
