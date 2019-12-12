using System;
using System.Runtime.Serialization;

namespace _2019TobbformosMvcPizzaEgyTabla
{
    [Serializable]
    internal class ModelMegrendeloNotValidLocationExeption : Exception
    {
        public ModelMegrendeloNotValidLocationExeption()
        {
        }

        public ModelMegrendeloNotValidLocationExeption(string message) : base(message)
        {
        }

        public ModelMegrendeloNotValidLocationExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ModelMegrendeloNotValidLocationExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}