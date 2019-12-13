using System;
using System.Runtime.Serialization;

namespace _2019TobbformosMvcPizzaEgyTabla
{
    [Serializable]
    internal class ModelMegrendeloNotValidLocationException : Exception
    {
        public ModelMegrendeloNotValidLocationException()
        {
        }

        public ModelMegrendeloNotValidLocationException(string message) : base(message)
        {
        }

        public ModelMegrendeloNotValidLocationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ModelMegrendeloNotValidLocationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}