using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace ProyectosArqui.Core.DTO
{
    public class EliminarArchivoDTO
    {


        [Required]
        public int id_proyecto { get; set; }

        [Required]
        public int id_archivo { get; set; }

    }
}
