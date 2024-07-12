using System;
using System.ComponentModel.DataAnnotations;

namespace Proyectos.DB
{
    public class Tags
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Texto { get; set; }


    }
}
