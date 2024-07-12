using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Proyectos.DB
{
    public class Archivos
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int id_proyecto { get; set; }

        [Required]
        public int id_dueno { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Ruta { get; set; }

        // Cambiar a tipo DateTime y configurar fecha y hora actual
        [Required]
        public DateTime Creacion { get; set; } = DateTime.Now;
        [Required]
        public string contenido { get; set; }


    }
}
