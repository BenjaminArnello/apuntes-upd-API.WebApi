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
    public class Mail
    {
        [Key]
        public string Correo { get; set; }
        [Required]
        public int idProyecto { get; set; }
    }
}
