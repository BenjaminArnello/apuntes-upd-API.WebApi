using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectosArqui.Core.DTO
{
    public class ComentariosDTO
    {
        public string NombreCompleto { get; set; }

        public string Contenido { get; set; }

        [Required]
        public DateTime Creacion { get; set; } = DateTime.Now;


    }
}
