using System;

namespace MoonSharp.Parser {
    class Token {
        private readonly TokenType type;
        private readonly String lexeme;
        private readonly int line;


        public Token(TokenType type, string lexeme, int line) {
            this.type = type;
            this.lexeme = lexeme;
            this.line = line;
        }

        public override string ToString() =>
            "Line " + line + ": " +  lexeme + ": " + type;
    }
}
