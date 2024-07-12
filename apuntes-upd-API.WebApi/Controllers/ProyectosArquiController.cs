using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Proyectos.DB;

using ProyectosArqui.Core;
using ProyectosArqui.Core.CustomExceptions;
using ProyectosArqui.Core.DTO;
using System.Security.Claims;

namespace apuntes_upd_API.WebApi.Controllers

{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProyectosArquiController : ControllerBase
    {
        private IProyectosArquiServices _proyectosArquiServices;
        public ProyectosArquiController(IProyectosArquiServices proyectosArquiServices)
        {
            _proyectosArquiServices = proyectosArquiServices;
        }

        [HttpGet]

        public IActionResult GetProyectos()
        {
            return Ok(_proyectosArquiServices.GetProyectos());
        }

        [HttpGet("ProyectosUsuario")]

        public IActionResult GetProyectosUsuario()
        {
            return Ok(_proyectosArquiServices.GetProyectosUsuario());
        }


        [HttpPost("getDetalleReferencia")]

        public IActionResult GetDetalleReferencia(ProyectoId idRef)
        {
            return Ok(_proyectosArquiServices.getDetalleReferencia(idRef));
        }



        [HttpPost]

        public IActionResult CrearProyecto(ProyectosArqui.Core.DTO.ProyectosTagsCrear request)
        {
            
                // Llamar al servicio para crear el proyecto
                var newProyecto = _proyectosArquiServices.CrearProyecto(request);

     

                // Retornar el proyecto creado con un código de respuesta Created (201)
                return Ok(newProyecto);


        }


        [HttpDelete]

        public IActionResult borrarProyecto(ProyectoId proyecto)
        {
            _proyectosArquiServices.borrarProyecto(proyecto);
            return Ok();
        }

        [HttpPut]

        public IActionResult EditarProyecto(ProyectosArqui.Core.DTO.Proyectos proyecto)
        {
            return Ok(_proyectosArquiServices.EditarProyecto(proyecto));
        }



        [HttpPost("comentar")]

        public IActionResult Comentar(ComentarioCrearDTO comentario)
        {
            return Ok(_proyectosArquiServices.Comentar(comentario));
        }


        [HttpPost("anadirTag")]

        public IActionResult añadirTagProyecto(Tags tag, int id_proyecto)
        {
            return Ok(_proyectosArquiServices.añadirTagProyecto(tag, id_proyecto));
        }


        [HttpPost("datosProyecto")]
        public async Task<IActionResult> getDatosProyecto(ProyectoId proyectoId)
        {
            return Ok(_proyectosArquiServices.GetDetallesProyecto(proyectoId));
        }

        [HttpPost("crearArchivo")]
        public async Task<IActionResult> crearArchivo(ProyectosArqui.Core.DTO.ArchivoDTO archivo)
        {
            return Ok(_proyectosArquiServices.crearArchivo(archivo));
        }


        [HttpPost("crearReferencia")]
        public async Task<IActionResult> crearReferencia(Referencias referencias)
        {
            return Ok(_proyectosArquiServices.crearReferencia(referencias));
        }


        [HttpDelete("eliminarReferencia")]
        public async Task<IActionResult> eliminarReferencia(ProyectoId refId)
        {
            _proyectosArquiServices.eliminarReferencia(refId);
            return Ok();
        }



        [HttpPost("añadirColaborador")]
        public async Task<IActionResult> añadirColaborador(Mail mail)
        {
            
            return Ok(_proyectosArquiServices.añadirColaborador(mail));
        }

        [HttpDelete("eliminarColaborador")]
        public async Task<IActionResult> eliminarColaborador(Mail mail)
        {
            _proyectosArquiServices.eliminarColaborador(mail);
            return Ok();
        }


        [HttpDelete("eliminarArchivo")]
        public async Task<IActionResult> eliminarArchvio(EliminarArchivoDTO archivo)
        {
            _proyectosArquiServices.eliminarArchivo(archivo);
            return Ok();
        }






    }
}
