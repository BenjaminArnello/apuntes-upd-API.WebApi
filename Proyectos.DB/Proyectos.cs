using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Proyectos.DB
{
    public class Proyectos
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [ForeignKey("idUsuario")]
        public Usuarios Usuario { get; set; }

        public string Descripcion { get; set; }

        // Cambiar a tipo DateTime y configurar fecha y hora actual
        [Required]
        public DateTime Creacion { get; set; } = DateTime.Now;

        public string Foto { get; set; }

        public bool Activo { get; set; }    

    }
}
