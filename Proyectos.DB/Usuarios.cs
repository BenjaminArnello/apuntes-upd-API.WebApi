using System.ComponentModel.DataAnnotations;

namespace Proyectos.DB
{
    public class Usuarios
    {
        [Key]
        public int id { get; set; }
        public required string Nombre {  get; set; }
        public required string ApellidoPat { get; set; }
        public required string ApellidoMat { get; set; }
        public bool Rol { get; set; }
        public string Clave { get; set; }
        public DateTime Creacion { get; set; } = DateTime.Now;
        public bool Activo { get; set; }


        public required string Mail {  get; set; }


    }
}
