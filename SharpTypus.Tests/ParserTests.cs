using SharpTypus.Parsing;
using SharpTypus.Parsing.Expressions;
using System;
using System.Collections.Generic;
using Xunit;
using static SharpTypus.Parsing.TokenType;

namespace SharpTypus.Tests {
    public class ParserTests {
        [Fact]
        public void CanParseValidLiteral() {
            var literals = new List<Token>() {
                new Token(Integer, "12", 1)
            };

            var excepted = new Literal(literals[0]);

            var parser = new Parser(literals);
            var actual = parser.Parse();

            Assert.Equal(excepted, actual);
        }

        [Fact]
        public void CanParseSImpleGroupingExpresion() {
            var intToken = new Token(Integer, "123", 1);
            var literals = new List<Token>() {
                new Token(LeftParen, "(", 1),
                intToken,
                new Token(RightParen, ")", 1)
            };

            var expected = new Grouping(new Literal(intToken));

            var parser = new Parser(literals);
            var actual = parser.Parse();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanParseUnaryExpresion() {
            var intToken = new Token(Integer, "123", 1);
            var literals = new List<Token>() {
                new Token(Minus, "-", 1),
                intToken,
            };

            var expected = new Unary(new Literal(intToken), new Token(Minus, "-", 1));

            var parser = new Parser(literals);
            var actual = parser.Parse();
            Assert.Equal(expected, actual);
        }
    }
}
