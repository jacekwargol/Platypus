using SharpTypus.Expressions;
using SharpTypus.Statements;
using System;
using System.Collections.Generic;
using static SharpTypus.Parsing.TokenType;

namespace SharpTypus.Parsing {
    class Parser {
        private List<Token> tokens;
        private int current;

        public Parser(List<Token> tokens) => this.tokens = tokens;

        public List<Statement> Parse() {
            var statements = new List<Statement>();

            while(!IsAtEnd()) {
                statements.Add(Declaration());
            }

            return statements;
        }


        private delegate Expr exprMethod<out Expr>();

        private Statement Declaration() {
            try {
                return TryMatchAndAdvance(Let) ? VarDeclaration() : Statement();
            }
            catch(ParsingException ex) {
                Synchronize();
                return null;
            }
        }

        private Statement VarDeclaration() {
            MatchOrThrowError("Expected variable name.", Identifier);

            var name = Previous();

            MatchOrThrowError("Invalid token.", Colon);
            MatchOrThrowError("Expected variable type.", I32, F64, False, True, StringToken);

            var type = Previous().Type;
            Expr initializer = null;
            if(TryMatch(Equal)) {
                initializer = Expression();
            }

            MatchOrThrowError("Expected ';' after cariable declaration.");

            return new LetStatement(name, type, initializer);
        }

        private Statement Statement() {
            return ExprStatement();
        }

        private Statement ExprStatement() {
            var expr = Expression();
            MatchOrThrowError("Expected ';' after expression.", Semicolon);
            return new ExprStatement(expr);
        }

        private Expr Expression() {
            return Equality();
        }

        private Expr Equality() {
            return ParseBinaryExpr(Comparison, EqualEqual, BangEqual);
        }

        private Expr Comparison() {
            return ParseBinaryExpr(Additive, Less, LessEqual, Greater, GreaterEqual);
        }

        private Expr Additive() {
            return ParseBinaryExpr(Multiplicative, Plus, Minus);
        }

        private Expr Multiplicative() {
            return ParseBinaryExpr(Unary, Star, Slash);
        }

        private Expr Unary() {
            if(!TryMatchAndAdvance(Bang, Minus)) {
                return Primary();
            }
            var op = Previous();
            return new Unary(Unary(), op);
        }

        private Expr Primary() {
            if(TryMatchAndAdvance(LeftParen)) {
                return Grouping();
            }

            return Literal();
        }

        private Expr Grouping() {
            var expr = Expression();
            MatchOrThrowError("Missing ')' after expression.", LeftParen);
            return new Grouping(expr);
        }

        private Expr Literal() {
            if(TryMatch(I32, F64, StringToken, True, False)) {
                return new Literal(Advance());
            }

            throw Exception("Invalid token.");
        }



        private Expr ParseBinaryExpr(Func<Expr> exprMethod, params TokenType[] operators) {
            var left = exprMethod();
            while(TryMatchAndAdvance(operators)) {
                var operator_ = Previous();
                var right = exprMethod();
                left = new Binary(left, right, operator_);
            }

            return left;
        }

        private bool TryMatch(params TokenType[] types) {
            foreach(var type in types) {
                if(tokens[current].Type == type) {
                    return true;
                }
            }

            return false;
        }

        private bool TryMatchAndAdvance(params TokenType[] types) {
            if(TryMatch(types)) {
                Advance();
                return true;
            }

            return false;
        }

        private void MatchOrThrowError(string message, params TokenType[] tokens) {
            if(TryMatchAndAdvance(tokens)) {
                return;
            }

            throw Exception(message);
        }

        private ParsingException Exception(string message) {
            Platypus.GenerateException(tokens[current], message);
            return new ParsingException(message);
        }

        // Try advancing to the next statement after ecountering parsing error
        private void Synchronize() {
            Advance();

            while(!IsAtEnd()) {
                switch(tokens[current].Type) {
                    case Class:
                    case Fun:
                    case For:
                    case While:
                    case If:
                    case Return:
                    case Let:
                        return;
                }

                if(Previous().Type == Semicolon || Previous().Type == RightBracket) {
                    Advance();
                    return;
                }

            }
        }
        private Token Advance() {
            if(IsAtEnd()) {
                return tokens[current];
            }

            current++;
            return Previous();
        }

        private Token Previous() {
            return current > 0 ? tokens[current - 1] : tokens[current];
        }

        private bool IsAtEnd() => current >= tokens.Count - 1;
    }
}
