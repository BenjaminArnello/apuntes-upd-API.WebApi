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
    public class CrarProyecto
    {
 
        [Required]
        public string Titulo { get; set; }

        public string Foto { get; set; }

    }
}
