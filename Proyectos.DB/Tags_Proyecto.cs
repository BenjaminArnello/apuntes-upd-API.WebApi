using System;
using System.ComponentModel.DataAnnotations;

namespace Proyectos.DB
{
    public class Tags_Proyecto
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int id_proyecto { get; set; }

        [Required]
        public int id_tag { get; set; } 


    }
}
