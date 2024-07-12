using System;
using System.ComponentModel.DataAnnotations;

namespace Proyectos.DB
{
    public class Miembros
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int id_proyecto { get; set; }

        [Required]
        public int id_usuario { get; set; }


    }
}
