using SharpTypus.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpTypus.Parsing {
    class AstPrinter : IExprVisitor<String> {

        // Print expression in polish notation
        public string Print(Expr expr) {
            return expr?.Accept(this);
        }

        public String Visit(Binary expr) {
            var exprSB = new StringBuilder();
            exprSB.Append(expr.Operator_).Append(' ');
            exprSB.Append(expr.LeftExpr.Accept(this));
            exprSB.Append(expr.RightExpr.Accept(this));

            return exprSB.ToString();
        }

        public String Visit(Unary expr) => expr.Operator_.ToString() + expr.Expr.Accept(this) + ' ';

        public String Visit(Literal expr) => expr.Token.ToString() + ' ';

        public String Visit(Grouping expr) => expr.Expr.Accept(this);
        public string Visit(Variable expr) => throw new NotImplementedException();
    }
}
