using SharpTypus.Parsing.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpTypus.Parsing {
    class AstPrinter : IExprVisitor<String> {
        public string Print(Expr expr) {
            return expr.Accept(this);
        }

        public String Visit(Binary expr) => throw new NotImplementedException();
        public String Visit(Unary expr) => throw new NotImplementedException();
        public String Visit(Literal expr) => throw new NotImplementedException();
        public String Visit(Grouping expr) => throw new NotImplementedException();
    }
}
