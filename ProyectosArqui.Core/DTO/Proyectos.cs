using Proyectos.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectosArqui.Core.DTO
{
    public class Proyectos
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        // Cambiar a tipo DateTime y configurar fecha y hora actual
        [Required]
        public DateTime Creacion { get; set; } = DateTime.Now;

        public string Foto { get; set; }

        public bool Activo { get; set; }

        public static explicit operator Proyectos(global::Proyectos.DB.Proyectos p) => new Proyectos
        {
            id = p.id,
            Titulo = p.Titulo,
            Descripcion = p.Descripcion,
            Creacion = p.Creacion,
            Foto = p.Foto,
            Activo = p.Activo
        };
    }
}
