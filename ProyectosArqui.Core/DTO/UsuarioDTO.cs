using Proyectos.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectosArqui.Core.DTO
{
    public class UsuarioDTO
    {

        public string Nombre { get; set; }
        public string ApellidoPat { get; set; }
        public string ApellidoMat { get; set; }

        public string Mail { get; set; }

        public static explicit operator UsuarioDTO(global::Proyectos.DB.Usuarios u) => new UsuarioDTO
        {
            Nombre = u.Nombre,
            ApellidoPat = u.ApellidoPat,
            ApellidoMat = u.ApellidoMat,
            Mail = u.Mail
        };
    }
}
