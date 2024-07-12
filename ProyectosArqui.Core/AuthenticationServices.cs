using Azure.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Proyectos.DB;
using ProyectosArqui.Core.CustomExceptions;
using ProyectosArqui.Core.DTO;
using ProyectosArqui.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectosArqui.Core
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;



        public AuthenticationServices(AppDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<UsuarioAutenticado> Login(string correo, string clave)
        {
            var dBUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Mail == correo);

            if (dBUser == null || _passwordHasher.VerifyHashedPassword(dBUser.Clave, clave) == PasswordVerificationResult.Failed)
            {
                throw new CredencialesIncorrectasExcepción("Correo o contraseña incorrectos");
            }

            string rolString = dBUser.Rol ? "user" : "admin";

            string fullname = dBUser.Nombre + " " + dBUser.ApellidoPat + " " + dBUser.ApellidoMat;

            return new UsuarioAutenticado
            {
                Nombre = dBUser.Nombre,
                ApellidoMat = dBUser.ApellidoMat,
                ApellidoPat = dBUser.ApellidoPat,
                Mail = dBUser.Mail,
                Rol = dBUser.Rol,
              
                Token = JwtGenerator.GenerateUserToken(dBUser.Mail,  rolString, fullname)

            };
        }

        public async Task<UsuarioAutenticado> Registro(Usuarios user)
        {
            var checkUser = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Mail.Equals(user.Mail));

            if (checkUser != null) 
            {
                throw new UsuarioYaExisteException("Mail Ya Existe");
            }

            user.Clave = _passwordHasher.HashPassword(user.Clave);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            string fullname = user.Nombre + " " + user.ApellidoPat + " " + user.ApellidoMat;

            return new UsuarioAutenticado
            {
                Nombre = user.Nombre,
                ApellidoPat = user.ApellidoPat,
                ApellidoMat = user.ApellidoMat,
                Mail = user.Mail,
                Token = JwtGenerator.GenerateUserToken(user.Mail, "user", fullname)
            };
        }
    }
}
