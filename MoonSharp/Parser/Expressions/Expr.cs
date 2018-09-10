namespace MoonSharp.Parser.Expressions {
    abstract class Expr {
        public abstract void Accept(IExprVisitor visitor);
    }
}
