namespace MoonSharp.Parsing.Expressions {
    abstract class Expr {
        public abstract T Accept<T>(IExprVisitor<T> visitor);
    }
}
