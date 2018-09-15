using SharpTypus.Parsing;
using System;

namespace SharpTypus {
    class RuntimeException : Exception {
        private readonly Token token;
        public RuntimeException() {
        }

        public RuntimeException(Token token, string message) : base(message) {
            this.token = token;
        }

        public RuntimeException(string message) : base(message) {
        }

        public RuntimeException(string message, Exception innerException) : base(message, innerException) {
        }

    }
}
