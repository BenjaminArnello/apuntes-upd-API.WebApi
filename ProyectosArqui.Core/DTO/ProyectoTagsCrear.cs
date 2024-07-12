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
    public class ProyectosTagsCrear
    {
        public ProyectosArqui.Core.DTO.Proyectos proyecto { get; set; }

        public List<string> tagsProyecto { get; set; }


    }
}
