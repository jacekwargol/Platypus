using SharpTypus.Parsing;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using static SharpTypus.Parsing.TokenType;

namespace SharpTypus.Tests {
    public class LexerTests {
        [Fact]
        public void CanTokenizeInteger() {
            var i = "123";
            var expected = new Token(I32, i, 1);

            var lexer = new Lexer(i);
            var actual = lexer.Tokenize()[0];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanTokenizeFloats() {
            var s = "12. 12.34";
            var expected = new List<Token>() {
                new Token(F64, "12.", 1),
                new Token(F64, "12.34", 1)
            };

            var lexer = new Lexer(s);
            var actual = lexer.Tokenize();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanTokenizeString() {
            var s = "\"abc 123 ?!\"";
            var expected = new Token(StringToken, "\"abc 123 ?!\"", 1);
            var lex = new Lexer(s);
            var actual = lex.Tokenize()[0];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanTokenizeIdentifiers() {
            var s = "abc a_ a1 a? _a and";
            var expected = new List<Token>() {
                new Token(Identifier, "abc", 1),
                new Token(Identifier, "a_", 1),
                new Token(Identifier, "a1", 1),
                new Token(Identifier, "a?", 1),
                new Token(Identifier, "_a", 1),
                new Token(And, "and", 1)
            };

            var lex = new Lexer(s);
            var actual = lex.Tokenize();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanTokenizeSimpleExpression() {
            var addition = "1 + 2";
            var expected = new List<Token>() {
                new Token(I32, "1", 1),
                new Token(Plus, "+", 1),
                new Token(I32, "2", 1)
            };

            var lexer = new Lexer(addition);
            var actual = lexer.Tokenize();

            Assert.Equal(expected, actual);
        }
    }
}
