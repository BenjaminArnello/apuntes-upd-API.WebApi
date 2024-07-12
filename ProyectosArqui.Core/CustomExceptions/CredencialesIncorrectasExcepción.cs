using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProyectosArqui.Core.CustomExceptions
{
    public class CredencialesIncorrectasExcepción : Exception
    {
        public CredencialesIncorrectasExcepción()
        {
        }

        public CredencialesIncorrectasExcepción(string? message) : base(message)
        {
        }

        public CredencialesIncorrectasExcepción(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CredencialesIncorrectasExcepción(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
