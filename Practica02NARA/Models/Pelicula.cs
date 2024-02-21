using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Practica02NARA.Models
{
    public partial class Pelicula
    {
        public int Id { get; set; }
        [Display(Name ="Nombre De La Peicula")]
        public string NombrePelicula { get; set; } = null!;
        public string Genero { get; set; } = null!;
        [Display(Name = "Año Publicada")]
        public string AñoPublicada { get; set; } = null!;
        public byte[]? ImagenPelicula { get; set; }
        [Display(Name = "Director")]
        public int DirectorPelicula { get; set; }

        [Display(Name = "Director")]
        public virtual Director DirectorPeliculaNavigation { get; set; } = null!;
    }
}
