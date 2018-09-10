using System;
using System.Collections.Generic;
using System.Text;

namespace MoonSharp.Parser.Expressions {
    class Unary : Expr {
        private readonly Token operator_;
        private readonly Expr expr;

        public Unary(Token operator_, Expr expression) {
            this.operator_ = operator_;
            expr = expression;
        }
    }
}
