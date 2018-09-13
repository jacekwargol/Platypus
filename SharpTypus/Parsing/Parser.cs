using SharpTypus.Parsing.Expressions;
using System;
using System.Collections.Generic;
using static SharpTypus.Parsing.TokenType;

namespace SharpTypus.Parsing {
    class Parser {
        private List<Token> tokens;
        private int current;

        public Parser(List<Token> tokens) => this.tokens = tokens;

        public Expr Parse() {
            try {
                return Expression();
            }
            catch(ParsingException ex) {
                return null;
            }
        }


        private delegate Expr exprMethod<out Expr>();

        private Expr Expression() {
            return Equality();
        }

        private Expr Equality() {
            return ParseBinaryExpr(Comparison, EqualEqual, BangEqual);
        }

        private Expr Comparison() {
            return ParseBinaryExpr(Additive, Plus, Minus);
        }

        private Expr Additive() {
            return ParseBinaryExpr(Multiplicative, Star, Slash);
        }

        private Expr Multiplicative() {
            return ParseBinaryExpr(Unary, Bang, Minus);
        }

        private Expr Unary() {
            if(!IsMatching(Bang, Minus)) {
                return Primary();
            }

            return new Unary(Unary(), Previous());
        }

        private Expr Primary() {
            if(IsMatching(LeftParen)) {
                return Grouping();
            }

            return Literal();
        }

        private Expr Grouping() {
            var expr = Expression();
            Consume(RightParen, "Missing ')' after expression.");

            return new Grouping(expr);
        }

        private Token Consume(TokenType token, string message) {
            if(IsMatching(token)) {
                return Advance();
            }

            throw new ParsingException(message);
        }

        private Expr Literal() {
            return new Literal(tokens[current]);
        }



        private Expr ParseBinaryExpr(Func<Expr> exprMethod, params TokenType[] operators) {
            var left = exprMethod();

            while(IsMatching(operators)) {
                var operator_ = Previous();
                var right = exprMethod();
                left = new Binary(left, right, operator_);
            }

            return left;
        }

        private bool IsMatching(params TokenType[] types) {
            if(IsAtEnd()) {
                return false;
            }

            foreach(var type in types) {
                if(tokens[current].Type == type) {
                    Advance();
                    return true;
                }
            }

            return false;
        }

        // Try advancing to the next statement after ecountering parsing error
        private void Sunchronize() {
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
            if(!IsAtEnd()) current++;
            return Previous();
        }

        private Token Previous() {
            return tokens[current - 1];
        }

        private bool IsAtEnd() => current >= tokens.Count;
    }
}
