// Class generated by GenerateExpressions.py script

using SharpTypus.Parsing;
using SharpTypus.Expressions;
namespace SharpTypus.Statements {
class LetStatement : Statement {
public Token Name { get; }
public Expr Initializer { get; }

public LetStatement(Token name, Expr initializer) {
Name = name;
Initializer = initializer;
}

public override T Accept<T>(IStatementVisitor<T> visitor) => visitor.Visit(this);
public override bool Equals(object obj) {
if(!(obj is LetStatement)) return false;
return ((LetStatement)obj).Name == this.Name && ((LetStatement)obj).Initializer == this.Initializer;
}
public override int GetHashCode() => (Name, Initializer).GetHashCode();
}
}
