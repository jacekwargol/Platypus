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

        [Fact]
        public void CanParseSimpleMultiplicativeExpression() {
            var floatToken = new Token(Float, "1.0", 1);
            var opToken = new Token(Star, "*", 1);
            var tokens = new List<Token>() {
                floatToken,
                opToken,
                floatToken
            };

            var expected = new Binary(new Literal(floatToken), new Literal(floatToken), opToken);

            var parser = new Parser(tokens);
            var actual = parser.Parse();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanParseComplexBinaryExpression() {
            var floatToken = new Token(Float, "1.0", 1);
            var tokens = new List<Token>() {
                floatToken,
                new Token(Star, "*", 1),
                floatToken,
                new Token(Plus, "+", 1),
                floatToken,
                new Token(EqualEqual, "==", 1),
                floatToken,
                new Token(Less, "<", 1),
                floatToken
            };

            var mult = new Binary(new Literal(floatToken), new Literal(floatToken), new Token(Star, "*", 1));
            var left = new Binary(mult, new Literal(floatToken), new Token(Plus, "+", 1));
            var right = new Binary(new Literal(floatToken), new Literal(floatToken), new Token(Less, "<", 1));

            var expected = new Binary(left, right, new Token(EqualEqual, "==", 1));

            var parser = new Parser(tokens);
            var actual = parser.Parse();

            Assert.Equal(expected, actual);
        }
    }
}
