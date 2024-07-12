using Proyectos.DB;
using ProyectosArqui.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ProyectosArqui.Core
{
    public interface IProyectosArquiServices
    {

        List<ProyectosArqui.Core.DTO.ProyectosTags> GetProyectos();
        List<ProyectosArqui.Core.DTO.ProyectosTags> GetProyectosUsuario();
        ProyectosArqui.Core.DTO.Proyectos CrearProyecto(ProyectosTagsCrear proyecto);

        Referencias getDetalleReferencia(ProyectoId id);


        ProyectosArqui.Core.DTO.Proyectos EditarProyecto(ProyectosArqui.Core.DTO.Proyectos proyecto);
        void borrarProyecto(ProyectoId proyecto);

        ProyectosArqui.Core.DTO.DetallesProyecto GetDetallesProyecto(ProyectoId id);

       ComentarioCrearDTO Comentar(ComentarioCrearDTO comentario);



        List<Proyectos.DB.Tags> GetTags();
        List<Proyectos.DB.Tags> GetTagsProyecto(int id);

        ProyectosArqui.Core.DTO.ArchivoDTO crearArchivo(ProyectosArqui.Core.DTO.ArchivoDTO archivo);

        Proyectos.DB.Tags añadirTagProyecto(Proyectos.DB.Tags tag, int id_proyecto);

        Referencias crearReferencia (Referencias referencias);

 
        Miembros añadirColaborador(Mail mail);

        void eliminarColaborador(Mail mail);

        void eliminarArchivo(EliminarArchivoDTO archivo);

        void eliminarReferencia(ProyectoId refId);







    }
}
