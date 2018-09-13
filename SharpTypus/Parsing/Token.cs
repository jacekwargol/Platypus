using System;

namespace SharpTypus.Parsing {
    class Token {
        public TokenType Type { get; }

        private readonly String lexeme;
        private readonly int line;


        public Token(TokenType type, string lexeme, int line) {
            Type = type;
            this.lexeme = lexeme;
            this.line = line;
        }


        public override string ToString() =>
            "Line " + line + ": " +  lexeme + ": " + Type;
    }
}
