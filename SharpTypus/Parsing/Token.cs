using System;

namespace SharpTypus.Parsing {
    class Token {
        public TokenType Type { get; }
        public string Lexeme { get; }
        public int Line { get; }

        public Token(TokenType type, string lexeme, int line) {
            Type = type;
            Lexeme = lexeme;
            Line = line;
        }


        public override string ToString() => Lexeme;

        public static bool operator ==(Token left, Token right) =>
            left.Type == right.Type && left.Lexeme == right.Lexeme;

        public static bool operator !=(Token left, Token right) =>
            !(left == right);

        public override bool Equals(object obj) => obj is Token ? (Token)obj == this : false;

        public override int GetHashCode() => (Type, Lexeme, Line).GetHashCode();

    }
}
