using System;
using System.Collections.Generic;
using System.Text;

namespace MoonSharp.Parser.Expressions {
    class Binary : Expr {
        private readonly Expr leftExpr;
        private readonly Expr rightExpr;
        private readonly Token operator_;

        public Binary(Expr leftExpression, Token operator_, Expr rightExpression) {
            leftExpr = leftExpression;
            rightExpr = rightExpression;
            this.operator_ = operator_;
        }
    }
}
