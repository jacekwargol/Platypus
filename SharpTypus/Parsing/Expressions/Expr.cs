namespace SharpTypus.Parsing.Expressions {
    abstract class Expr {
        public abstract T Accept<T>(IExprVisitor<T> visitor);

        public abstract override bool Equals(object obj);
        public abstract override int GetHashCode();

        public static bool operator ==(Expr left, Expr right) => left.Equals(right);
        public static bool operator !=(Expr left, Expr right) => !left.Equals(right);



    }
}
