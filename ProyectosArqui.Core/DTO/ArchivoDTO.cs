using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace ProyectosArqui.Core.DTO
{
    public class ArchivoDTO
    {


        [Required]
        public int id_proyecto { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Ruta { get; set; }

        [Required]
        public string contenido { get; set; }


    }
}
