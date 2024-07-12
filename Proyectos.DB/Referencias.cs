using System;
using System.ComponentModel.DataAnnotations;

namespace Proyectos.DB
{
    public class Referencias
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Titulo{ get; set; }

        [Required]
        public int id_proyecto { get; set; }

        [Required]
        public string Foto { get; set; }

        [Required]  
        public string Descripcion { get; set; }


    }
}
