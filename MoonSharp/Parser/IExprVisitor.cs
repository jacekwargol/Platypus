using MoonSharp.Parser.Expressions;

namespace MoonSharp.Parser {
    interface IExprVisitor {
        void Visit(Binary expr);
        void Visit(Unary expr);
    }
}
