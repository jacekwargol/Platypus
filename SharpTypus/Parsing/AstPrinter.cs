using SharpTypus.Parsing.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpTypus.Parsing {
    class AstPrinter : IExprVisitor<String> {

        // Print expression in polish notation
        public string Print(Expr expr) {
            return expr.Accept(this);
        }

        public String Visit(Binary expr) {
            var exprSB = new StringBuilder();
            exprSB.Append(expr.Operator_).Append(' ');
            exprSB.Append(expr.LeftExpr.Accept(this)).Append(' ');
            exprSB.Append(expr.RightExpr.Accept(this)).Append(' ');

            return exprSB.ToString();
        }

        public String Visit(Unary expr) => expr.Operator_.ToString() + expr.Expr.ToString() + ' ';

        public String Visit(Literal expr) => expr.Token.ToString() + ' ';

        public String Visit(Grouping expr) => expr.Expr.Accept(this);
    }
}
