using System;

namespace SharpTypus.Parsing {
    class Token {
        public TokenType Type { get; }
        public string Lexeme { get; }
        public int Line { get; }

        public Token(TokenType type, string lexeme, int line) {
            Type = type;
            Lexeme = lexeme;
            this.Line = line;
        }


        public override string ToString() => Lexeme;
            //"Line " + line + ": " +  lexeme + ": " + Type;
    }
}
