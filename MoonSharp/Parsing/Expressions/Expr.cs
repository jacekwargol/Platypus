namespace MoonSharp.Parsing.Expressions {
    abstract class Expr {
        public abstract void Accept(IExprVisitor visitor);
    }
}
