using SharpTypus.Parsing;
using SharpTypus.Parsing.Expressions;
using static SharpTypus.Parsing.TokenType;
using Xunit;
using SharpTypus.Interpreting;

namespace SharpTypus.Tests {
    public class InterpreterTests {
        [Fact]
        public void CanIntepretInteger() {
            var expr = new Literal(new Token(Integer, "1", 1));
            var expected = 1;

            var interpreter = new Interpreter();
            var actual = interpreter.Visit(expr);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanIntepretFloat() {
            var expr = new Literal(new Token(Float, "2.1", 1));
            var expected = 1.0;

            var interpreter = new Interpreter();
            var actual = interpreter.Visit(expr);

            Assert.Equal(expected, actual);
        }
    }
}
