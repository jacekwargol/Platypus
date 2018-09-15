using SharpTypus.Parsing;
using SharpTypus.Parsing.Expressions;
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
    }
}
