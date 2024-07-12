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
    public class DetallesProyecto

    {
        public int Autoridad { get; set; }
  
        public ProyectosArqui.Core.DTO.ProyectosTagsCrear ProyectoTags { get; set; }

        public List<ComentariosDTO> Comentarios { get; set; }

        public List<Referencias> Referencias { get; set; }

        public List<Archivos> Archivos { get; set; }

        public List<ProyectosArqui.Core.DTO.UsuarioDTO> Colaboradores { get; set; }


    }
}
