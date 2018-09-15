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
            var expected = new Token(Integer, i, 1);

            var lexer = new Lexer(i);
            var actual = lexer.Tokenize()[0];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanTokenizeSimpleExpression() {
            var addition = "1 + 2";
            var expected = new List<Token>() {
                new Token(Integer, "1", 1),
                new Token(Plus, "+", 1),
                new Token(Integer, "2", 1)
            };

            var lexer = new Lexer(addition);
            var actual = lexer.Tokenize();

            Assert.True(expected.SequenceEqual(expected));
        }
    }
}
