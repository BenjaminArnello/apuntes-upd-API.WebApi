using System;
using System.ComponentModel.DataAnnotations;

namespace Proyectos.DB
{
    public class Comentarios
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int id_proyecto { get; set; }

        [Required]
        public int id_usuario { get; set; }

        [Required]

        public string Contenido { get; set; }

        [Required]
        public DateTime Creacion { get; set; } = DateTime.Now;


    }
}
