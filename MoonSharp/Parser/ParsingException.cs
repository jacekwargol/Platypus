using System;
using System.Runtime.Serialization;

namespace MoonSharp.Parser {
    class ParsingException : Exception {
        public ParsingException() {
        }

        public ParsingException(string message) : base(message) {
        }

        public ParsingException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
