using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectosArqui.Core.DTO
{
    public class UsuarioAutenticado
    {
        public string Token { get; set; }

        public string Nombre { get; set; }
        public string ApellidoPat { get; set; }
        public string ApellidoMat { get; set; }

        public string Mail { get; set; }

        public bool Rol {  get; set; }


    }
}
