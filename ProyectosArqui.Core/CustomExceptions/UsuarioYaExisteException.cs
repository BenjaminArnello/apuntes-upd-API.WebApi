

using System.Runtime.Serialization;

namespace ProyectosArqui.Core.CustomExceptions
{
    public class UsuarioYaExisteException : Exception
    {
        public UsuarioYaExisteException()
        {
        }

        public UsuarioYaExisteException(string? message) : base(message)
        {
        }

        public UsuarioYaExisteException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UsuarioYaExisteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
