using SharpTypus.Expressions;
using SharpTypus.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpTypus.Parsing {
    class AstPrinter : IExprVisitor<String>, IStatementVisitor<object> {
        // Print expression in polish notation
        public void Print(List<Statement> statements) {
            foreach(var statement in statements) {
                Console.WriteLine(statement.Accept(this));
            }
        }

        public String Visit(Binary expr) {
            var exprSB = new StringBuilder();
            exprSB.Append(expr.Operator_).Append(' ');
            exprSB.Append(expr.LeftExpr.Accept(this));
            exprSB.Append(expr.RightExpr.Accept(this));

            return exprSB.ToString();
        }

        public String Visit(Unary expr) => expr.Operator_.Lexeme + ' ' + expr.Expr.Accept(this);

        public String Visit(Literal expr) => expr.Token.ToString() + ' ';

        public String Visit(Grouping expr) => expr.Expr.Accept(this);

        public object Visit(ExprStatement statement) => statement.Expr.Accept(this);
        public string Visit(Variable expr) => throw new NotImplementedException();
        public object Visit(LetStatement expr) => throw new NotImplementedException();
    }
}
