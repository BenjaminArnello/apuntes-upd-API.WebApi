using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using Proyectos.DB;
using ProyectosArqui.Core.DTO;
using System.Reflection.Metadata;

namespace ProyectosArqui.Core
{
    public class ProyectosArquiServices : IProyectosArquiServices

    {
        private Proyectos.DB.AppDbContext _context;
        private readonly Proyectos.DB.Usuarios _usuario;
        public ProyectosArquiServices(Proyectos.DB.AppDbContext context, IHttpContextAccessor httpContextAccessor) 
        {
            _context = context;
            _usuario = _context.Usuarios
                .First(u => u.Mail == httpContextAccessor.HttpContext.User.Identity.Name);

        }
        public List<ProyectosArqui.Core.DTO.ProyectosTags> GetProyectos()
        {
            var proyectosDtoList = _context.Proyectos
                                           .Select(e => (ProyectosArqui.Core.DTO.Proyectos)e)
                                           .ToList();

            var proyectosTagsDtoList = new List<ProyectosArqui.Core.DTO.ProyectosTags>();

            foreach (var proyecto in proyectosDtoList)
            {
                var tagIds = _context.Tags_Proyecto
                                     .Where(tp => tp.id_proyecto == proyecto.id)
                                     .Select(tp => tp.id_tag)
                                     .ToList();

                var tags = _context.Tags
                                   .Where(t => tagIds.Contains(t.id))
                                   .ToList();

                proyectosTagsDtoList.Add(new ProyectosArqui.Core.DTO.ProyectosTags
                {
                    proyecto = (ProyectosArqui.Core.DTO.Proyectos)proyecto,
                    tagsProyecto = tags
                });
            }

            return proyectosTagsDtoList;
        }



        public ProyectosArqui.Core.DTO.Proyectos GetProyecto(int id) =>
      
            _context.Proyectos
                .Where(e => e.Usuario.id == _usuario.id && e.id == id)
                .Select(e => (ProyectosArqui.Core.DTO.Proyectos)e)
                .First();

        public List<ProyectosArqui.Core.DTO.ProyectosTags> GetProyectosUsuario()
        {
            var proyectosDtoList = _context.Proyectos
                                           .Where(e => e.Usuario == _usuario)
                                           .Select(e => (ProyectosArqui.Core.DTO.Proyectos)e)
                                           .ToList();

            var proyectosTagsDtoList = new List<ProyectosArqui.Core.DTO.ProyectosTags>();

            foreach (var proyecto in proyectosDtoList)
            {
                var tagIds = _context.Tags_Proyecto
                                     .Where(tp => tp.id_proyecto == proyecto.id)
                                     .Select(tp => tp.id_tag)
                                     .ToList();

                var tags = _context.Tags
                                   .Where(t => tagIds.Contains(t.id))
                                   .ToList();

                proyectosTagsDtoList.Add(new ProyectosArqui.Core.DTO.ProyectosTags
                {
                    proyecto = (ProyectosArqui.Core.DTO.Proyectos)proyecto,
                    tagsProyecto = tags
                });
            }

            return proyectosTagsDtoList;
        }


        public ProyectosArqui.Core.DTO.Proyectos CrearProyecto(ProyectosArqui.Core.DTO.ProyectosTagsCrear proyecto)
        {
            try
            {
                // Loguear información antes de la inserción
                Console.WriteLine($"Insertando proyecto: Titulo = {proyecto.proyecto.Titulo}, Foto = {proyecto.proyecto.Foto}");

                if (_usuario == null)
                {
                    return null;
                }

                // Create the project entity
                var proyectoInsert = new Proyectos.DB.Proyectos
                {
                    Titulo = proyecto.proyecto.Titulo,
                    Foto = proyecto.proyecto.Foto,
                    Usuario = _usuario,
                    Descripcion = "Descripción del Proyecto", // Ensure Descripcion is not overwritten if provided
                    Activo = true // Assuming Activo should be true by default
                };

                // Add project to context and save changes to get the project ID
                _context.Proyectos.Add(proyectoInsert);
                _context.SaveChanges();

                int proyectoId = proyectoInsert.id;

                // Loguear información después de la inserción
                Console.WriteLine($"Proyecto insertado correctamente: Id = {proyectoId}, Titulo = {proyectoInsert.Titulo}, Foto = {proyectoInsert.Foto}");

                // Process tags
                foreach (var tagNombre in proyecto.tagsProyecto)
                {
                    // Check if the tag already exists
                    var existingTag = _context.Tags.FirstOrDefault(t => t.Texto == tagNombre);

                    int tagId;

                    // If tag doesn't exist, insert it
                    if (existingTag == null)
                    {
                        var newTag = new Tags { Texto = tagNombre };
                        _context.Tags.Add(newTag);
                        _context.SaveChanges();
                        tagId = newTag.id;
                    }
                    else
                    {
                        tagId = existingTag.id;
                    }

                    // Link tag with the project using tags_proyecto table
                    _context.Tags_Proyecto.Add(new Tags_Proyecto { id_tag = tagId, id_proyecto = proyectoInsert.id });
                    _context.SaveChanges();
                }

                return (ProyectosArqui.Core.DTO.Proyectos)proyectoInsert;
            }
            catch (Exception ex)
            {
                // Manejo de errores: Loguear el error
                Console.WriteLine($"Error al insertar el proyecto: {ex.Message}");
                throw; // Propagar la excepción para manejo adicional si es necesario
            }
        }


        public void borrarProyecto(ProyectoId proyectoid)

        {
            if (_usuario.Rol == false)
            {
                var dbProyecto = _context.Proyectos.First(e => e.id == proyectoid.id);
                _context.Proyectos.Remove(dbProyecto);
                _context.SaveChanges();
            }
            else
            {
                var dbProyecto = _context.Proyectos.First(e => e.Usuario.id == _usuario.id && e.id == proyectoid.id);
                _context.Proyectos.Remove(dbProyecto);
                _context.SaveChanges();

            }

        }

        public Referencias getDetalleReferencia( ProyectoId idRef)
        {
            var dbProyecto = _context.Referencias.FirstOrDefault(e => e.id == idRef.id);
            return dbProyecto;
 

        }
        public ProyectosArqui.Core.DTO.Proyectos EditarProyecto(ProyectosArqui.Core.DTO.Proyectos proyecto)
        {
            var dbProyecto = _context.Proyectos.FirstOrDefault(e => e.Usuario.id == _usuario.id && e.id == proyecto.id);
            if (dbProyecto != null)
            {
                dbProyecto.Descripcion = proyecto.Descripcion;
                dbProyecto.Titulo = proyecto.Titulo;
                dbProyecto.Foto = proyecto.Foto;
                _context.SaveChanges();
                return (ProyectosArqui.Core.DTO.Proyectos)dbProyecto;

            }

            return null;

        }


        public ComentarioCrearDTO Comentar(ComentarioCrearDTO comentario)
        {
            var comentarioInsert = new Proyectos.DB.Comentarios
            {
                id_proyecto = comentario.idProyecto,
                id_usuario = _usuario.id,
                Contenido = comentario.Contenido,
            };

            _context.Comentarios.Add(comentarioInsert);
            _context.SaveChanges();

            return comentario;
        }

        public List<Tags> GetTags()
        {
            return _context.Tags.ToList();

        }

        public List<Tags> GetTagsProyecto(int id_proyecto)
        {
            // Assuming that Tags has a property named ProyectoId or similar that relates to the project ID
            List<Tags_Proyecto> tags_proyecto = _context.Tags_Proyecto.Where(t => t.id_proyecto== id_proyecto).ToList();
            List<int> tagIds = tags_proyecto.Select(tp => tp.id_tag).ToList();

            // Retrieve the tags whose IDs are in the tagIds list
            List<Tags> tags = _context.Tags.Where(t => tagIds.Contains(t.id)).ToList();


            return tags;
        }


        public ProyectosArqui.Core.DTO.ArchivoDTO crearArchivo(ProyectosArqui.Core.DTO.ArchivoDTO archivo)
        {
            var nuevoArchivo = new Proyectos.DB.Archivos
            {
                Nombre = archivo.Nombre,
                Ruta = archivo.Ruta,
                contenido = archivo.contenido,
                id_proyecto = archivo.id_proyecto,
                id_dueno = _usuario.id

            };
            _context.Archivos.Add(nuevoArchivo);
            _context.SaveChanges();

            return archivo;
        }
        public Tags añadirTagProyecto(Tags tag, int id_proyecto)
        {
            // Check if the tag already exists
            var existingTag = _context.Tags.FirstOrDefault(t => t.Texto == tag.Texto);

            if (existingTag == null)
            {
                // Tag does not exist, add it to the Tags table
                _context.Tags.Add(tag);
                _context.SaveChanges(); // Save changes to get the generated TagId
                existingTag = tag; // Now existingTag refers to the newly added tag
            }

            // Check if the tag is already linked to the project
            var existingLink = _context.Tags_Proyecto.FirstOrDefault(tp => tp.id_tag == existingTag.id && tp.id_proyecto == id_proyecto);

            if (existingLink == null)
            {
                // Link does not exist, add it to the Tags_Proyecto table
                var tagProyecto = new Tags_Proyecto
                {
                    id_tag = existingTag.id,
                    id_proyecto = id_proyecto
                };

                _context.Tags_Proyecto.Add(tagProyecto);
                _context.SaveChanges(); // Save changes to add the new link
            }

            return existingTag;
        }



        public void eliminarReferencia(ProyectoId refId)

        {
            var dbRef = _context.Referencias.First(e => e.id == refId.id);
            var proyecto = _context.Proyectos.First(e => e.id == dbRef.id_proyecto);

            if (_usuario.Rol == false)
            {

                _context.Referencias.Remove(dbRef);
                _context.SaveChanges();
                return;
            }
            else if (_usuario.id == proyecto.Usuario.id)
            {
                _context.Referencias.Remove(dbRef);
                _context.SaveChanges();
                return;
            }
            else
            {
                var miembro = _context.Miembros
                .First(e => e.id == _usuario.id && e.id_proyecto == proyecto.id);

                if (miembro != null)
                {
                    _context.Referencias.Remove(dbRef);
                    _context.SaveChanges();
                    return;

                }
                else return;

            }
        }


        public Referencias crearReferencia(Referencias referencia)
        {
            _context.Referencias.Add(referencia);
            _context.SaveChanges();
            return referencia;
        }

        public void eliminarArchivo(EliminarArchivoDTO archivo)
        {

            var dbArch = _context.Archivos.First(e => e.id == archivo.id_archivo);
            var proyecto = _context.Proyectos.First(e => e.id == archivo.id_proyecto);

            if (_usuario.Rol == false)
            {

                _context.Archivos.Remove(dbArch);
                _context.SaveChanges();
                return;
            }
            else if (_usuario.id == proyecto.Usuario.id)
            {
                _context.Archivos.Remove(dbArch);
                _context.SaveChanges();
                return;
            }
            else
            {
                var miembro = _context.Miembros
                .First(e => e.id == _usuario.id && e.id_proyecto == proyecto.id);

                if (miembro != null)
                {
                    _context.Archivos.Remove(dbArch);
                    _context.SaveChanges();
                    return;

                }
                else return;

            }
        }
        public Miembros añadirColaborador(Mail mail)


        {
            var usuario = _context.Usuarios.FirstOrDefault(e => e.Mail == mail.Correo);
            var proyecto = _context.Proyectos.FirstOrDefault(e => e.id == mail.idProyecto);

            if (usuario != null && _usuario.id == proyecto.Usuario.id)
            {
                var nuevoColaborador = new Proyectos.DB.Miembros
                {
                    id_usuario = usuario.id,
                    id_proyecto = mail.idProyecto

                };
                _context.Miembros.Add(nuevoColaborador);
                _context.SaveChanges();
                return nuevoColaborador;
            }
            else return null;
        }

        public void eliminarColaborador(Mail mail)


        {
            var usuario = _context.Usuarios.FirstOrDefault(e => e.Mail == mail.Correo);
            var proyecto = _context.Proyectos.FirstOrDefault(e => e.id == mail.idProyecto);

            if (usuario != null ) 
            {
                if(_usuario == proyecto.Usuario)
                {
                    var MiembroLink = _context.Miembros.FirstOrDefault(e => e.id_usuario == usuario.id && e.id_proyecto == proyecto.id);
                    if (MiembroLink != null)
                    {
                        _context.Miembros.Remove(MiembroLink);
                        _context.SaveChanges();
                        return;
                    }
                }

            }
            else return;
        }

        public DetallesProyecto GetDetallesProyecto(ProyectoId proyectoId)
        {
            var autoridad = 4;

            var proyecto = _context.Proyectos
                .Where(e => e.id == proyectoId.id)
                .Select(e => (Proyectos.DB.Proyectos)e)
                .First();


            List <Miembros> colaboradores = _context.Miembros
                .Where(e => e.id_proyecto == proyectoId.id).Select(e => e).ToList();


            List<UsuarioDTO> listMiembros = new List<UsuarioDTO>();

            UsuarioDTO dueño = _context.Usuarios
                .Where(e => e.id == _usuario.id)
                .Select(e => (UsuarioDTO)e)
                .First();

            listMiembros.Add((UsuarioDTO)_usuario);

            foreach (var colaborador in colaboradores)
            {
                // Trae los datos de los colaboradores
                var datosColaborador = _context.Usuarios.FirstOrDefault(c => c.id == colaborador.id_usuario);
                listMiembros.Add((UsuarioDTO)datosColaborador);
            
            }

            List<Tags_Proyecto> tagList = _context.Tags_Proyecto
                    .Where(t => t.id_proyecto == proyecto.id).ToList();

            List<string> tagsText = new List<string>();


            foreach (var tag in tagList)
            {
                // Check if the tag already exists
                var tagText = _context.Tags.FirstOrDefault(c => c.id == tag.id_tag);
                
                if (tagText != null) { tagsText.Add(tagText.Texto); }

            }


            List<Comentarios> comentariosList = _context.Comentarios
                .Where(e => e.id_proyecto == proyectoId.id).Select(e => e).ToList();

            List<ComentariosDTO> comentariosDTOList = new List<ComentariosDTO>();

            foreach (var comentario in comentariosList)
            {
                var usuario = _context.Usuarios.FirstOrDefault(N => N.id == comentario.id_usuario);
                if (usuario != null)
                {
                    var nombreCompleto = $"{usuario.Nombre} {usuario.ApellidoPat} {usuario.ApellidoMat}";
                    var nuevoComentarioDTO = new ComentariosDTO
                    {
                        NombreCompleto = nombreCompleto,
                        Contenido = comentario.Contenido,
                        Creacion = comentario.Creacion // Assuming comentario has a Creacion property
                    };

                    comentariosDTOList.Add(nuevoComentarioDTO);
                }
            }

            List<Referencias> referenciasList = _context.Referencias
                .Where(e => e.id_proyecto == proyectoId.id).Select(e => e).ToList();

            List<Proyectos.DB.Archivos> archivosList = _context.Archivos
                .Where(e => e.id_proyecto == proyectoId.id).Select(e => e).ToList();


            var isColaborador = 0;



            foreach (var colaborador in colaboradores)
            {
                var datosColaborador = _context.Usuarios.FirstOrDefault(c => c.id == colaborador.id_usuario);
                if (_usuario == datosColaborador)
                {
                    isColaborador ++;
                }
            }


            if (_usuario.Rol == false)
            {
                autoridad = 1;
            }

            else if (proyecto.Usuario == _usuario)
            {
                autoridad = 2;
            }

            else if (isColaborador == 1)
            {    
                autoridad = 3;
            }

            else { autoridad = 4;
            }

            var proyectotag = new ProyectosArqui.Core.DTO.ProyectosTagsCrear
            {
                proyecto = (ProyectosArqui.Core.DTO.Proyectos)proyecto,
                tagsProyecto = tagsText

            };

            var dataProyecto = new ProyectosArqui.Core.DTO.DetallesProyecto
            {
                Autoridad = autoridad,
                ProyectoTags = proyectotag,
                Comentarios = comentariosDTOList,
                Referencias = referenciasList,
                Archivos = archivosList,
                Colaboradores = listMiembros

            };



            return dataProyecto;

        }

    }
}
