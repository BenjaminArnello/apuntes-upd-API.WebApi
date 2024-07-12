using Microsoft.Identity.Client;
using Proyectos.DB;
using ProyectosArqui.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectosArqui.Core
{
    public interface IAuthenticationServices
    {
        Task<UsuarioAutenticado> Registro (Usuarios user);
        Task<UsuarioAutenticado> Login(string correo, string clave);

    }
}
