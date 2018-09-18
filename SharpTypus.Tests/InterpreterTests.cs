using SharpTypus.Parsing;
using SharpTypus.Parsing.Expressions;
using static SharpTypus.Parsing.TokenType;
using Xunit;
using SharpTypus.Interpreting;

namespace SharpTypus.Tests {
    public class InterpreterTests {
        [Fact]
        public void CanIntepretInteger() {
            var expr = new Literal(new Token(I32, "1", 1));
            var expected = 1;

            var interpreter = new Interpreter();
            var actual = interpreter.Visit(expr);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanIntepretFloat() {
            var expr = new Literal(new Token(F64, "2.1", 1));
            var expected = 2.1;

            var interpreter = new Interpreter();
            var actual = interpreter.Visit(expr);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanInterpretMinusUnary() {
            var expr = new Unary(new Literal(new Token(F64, "2.1", 1)), new Token(Minus, "-", 1));
            var expected = -2.1;

            var interpreter = new Interpreter();
            var actual = interpreter.Visit(expr);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanInterpretBangUnary() {
            var expr = new Unary(new Literal(new Token(False, "false", 1)), new Token(Bang, "!", 1));
            var expected = true;

            var interpreter = new Interpreter();
            var actual = interpreter.Visit(expr);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanInterpretIntFloatAddition() {
            var expr = new Binary(new Literal(new Token(I32, "3", 1)), new Literal(new Token(F64, "2.1", 1)),
                new Token(Plus, "+", 1));

            var expected = 5.1;

            var interpreter = new Interpreter();
            var actual = interpreter.Visit(expr);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanInterpretComplexAddition() {
            var right = new Binary(new Literal(new Token(F64, "2.0", 1)), new Literal(new Token(I32, "2", 1)), new Token(Star, "*", 1));
            var expr = new Binary(new Literal(new Token(I32, "1", 1)), right, new Token(Plus, "+", 1));

            var expected = 5.0;

            var interpreter = new Interpreter();
            var actual = interpreter.Visit(expr);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanConcatenateStrings() {
            var expr = new Binary(new Literal(new Token(StringToken, "ab", 1)), 
                new Literal(new Token(StringToken, "cd", 1)), new Token(Plus, "+", 1));

            var expected = "abcd";

            var interpreter = new Interpreter();
            var actual = interpreter.Visit(expr);

            Assert.Equal(expected, actual);
        }
    }
}
