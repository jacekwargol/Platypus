namespace SharpTypus.Statements {
    abstract class Statement {
        public abstract T Accept<T>(IStatementVisitor<T> visitor);

        public abstract override bool Equals(object obj);
        public abstract override int GetHashCode();

        public static bool operator ==(Statement left, Statement right) => left.Equals(right);
        public static bool operator !=(Statement left, Statement right) => !left.Equals(right);
    }
}
