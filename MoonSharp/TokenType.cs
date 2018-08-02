namespace MoonSharp {
    enum TokenType {
        // Single-character tokens
        LeftParen, RightParen, LeftBracket, RightBracket,
        Comma, Dot, Semicolon, Colon,
        Plus, Minus, Star, Slash,

        // Tokens that can be single- or double-characters
        Equal, EqualEqual, Bang, BangEqual,
        Less, Greater, LessEqual, GreaterEqual,

        // Literals
        Integer, StringToken, Bool,

        // Keywords
        And, Or, If, Else, Elif,
        True, False,
        Class, Fun,
        For, While,
        This, Base,
        Let,
        Return,

        EOF,
    }
}
