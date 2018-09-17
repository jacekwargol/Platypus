using SharpTypus.Parsing;
using SharpTypus.Parsing.Expressions;
using System;
using System.Globalization;

namespace SharpTypus.Interpreting {
    class Interpreter : IExprVisitor<object> {
        public void Interpret(Expr expr) {
            try {
                Console.WriteLine(Evaluate(expr));
            }
            catch(RuntimeException ex) {
                Platypus.GenerateRuntimeException(ex);
            }
        }


        public object Visit(Literal expr) {
            if(expr.Token.Type == TokenType.Integer) {
                if(Int32.TryParse(expr.Token.Lexeme, out int result)) {
                    return result;
                }
                throw new RuntimeException(expr.Token,
                    "Wrong token: " + expr.Token.Line);
            }

            if(expr.Token.Type == TokenType.Float) {
                if(Double.TryParse(expr.Token.Lexeme, NumberStyles.Any, CultureInfo.InvariantCulture, out double result)) {
                    return result;
                }
                throw new RuntimeException(expr.Token,
                    "Wrong token: " + expr.Token.Line);
            }

            return null;
        }

        public object Visit(Grouping expr) => Evaluate(expr.Expr);

        public object Visit(Unary expr) {
            var right = Evaluate(expr.Expr);

            if(expr.Operator_.Type == TokenType.Minus) {
                if(!IsNumber(right)) {
                    throw new RuntimeException("Operand must be a number.");
                }
                return (right is int ? -(int)right : -(double)right);
            }

            if(expr.Operator_.Type == TokenType.Bang) {
                if(right is bool) {
                    return (bool)right;
                }

                throw new RuntimeException("Operand must ba a bool.");
            }

            return null;
        }

        public object Visit(Binary expr) {
            var left = Evaluate(expr.LeftExpr);
            var right = Evaluate(expr.RightExpr);

            switch(expr.Operator_.Type) {
                case TokenType.Star:
                    return EvaluateMultiplication(left, right);
                case TokenType.Slash:
                    return EvaluateDivision(left, right);

                case TokenType.Plus:
                    return EvaluateAddition(left, right);
                case TokenType.Minus:
                    return EvaluateSubtraction(left, right);

                case TokenType.Less:
                    return EvaluateLessThan(left, right);
                case TokenType.Greater:
                    return EvaluateGreaterThan(left, right);
                case TokenType.LessEqual:
                    return EvaluateLessEqual(left, right);
                case TokenType.GreaterEqual:
                    return EvaluateGreaterEqual(left, right);

                case TokenType.EqualEqual:
                    return IsEqual(left, right);
                case TokenType.BangEqual:
                    return !IsEqual(left, right);
            }

            return null;
        }



        private object Evaluate(Expr expr) {
            return expr.Accept(this);
        }

        private object EvaluateGreaterEqual(object left, object right) {
            if(!IsNumber(left) || !IsNumber(right)) {
                throw new RuntimeException("Operands must be numbers.");
            }

            if(left is int && right is int) {
                return (int)left >= (int)right;
            }

            return (double)left >= (double)right;
        }

        private object EvaluateGreaterThan(object left, object right) {
            if(!IsNumber(left) || !IsNumber(right)) {
                throw new RuntimeException("Operands of multiplication must be numbers.");
            }

            return (double)left > (double)right;
        }

        private object EvaluateLessThan(object left, object right) {
            if(!IsNumber(left) || !IsNumber(right)) {
                throw new RuntimeException("Operands of multiplication must be numbers.");
            }

            return (double)left < (double)right;
        }

        private bool EvaluateLessEqual(object left, object right) {
            if(!IsNumber(left) || !IsNumber(right)) {
                throw new RuntimeException("Operands must be numbers.");
            }

            if(left is int && right is int) {
                return (int)left <= (int)right;
            }

            return (double)left <= (double)right;
        }

        private object EvaluateDivision(object left, object right) {
            if(!IsNumber(left) || !IsNumber(right)) {
                throw new RuntimeException("Operands of division must be numbers.");
            }

            if(left is int && right is int) {
                return (int)left / (int)right;
            }

            return (double)left / (double)right;
        }

        private object EvaluateMultiplication(object left, object right) {
            if(!IsNumber(left) || !IsNumber(right)) {
                throw new RuntimeException("Operands of multiplication must be numbers.");
            }

            if(left is int && right is int) {
                return (int)left * (int)right;
            }

            return (double)left * (double)right;
        }

        private object EvaluateSubtraction(object left, object right) {
            if(!IsNumber(left) || !IsNumber(right)) {
                throw new RuntimeException("Operands of substraction must be numbers.");
            }

            if(left is int && right is int) {
                return (int)left - (int)right;
            }

            return (double)left - (double)right;
        }

        private object EvaluateAddition(object left, object right) {
            if((!IsNumber(left) || !IsNumber(right)) ||
                (!(left is string) || !(right is string))) {
                throw new RuntimeException("Operands of addition must be numbers or strings.");
            }
            if(left is int && right is int) {
                return (int)left + (int)right;
            }

            // + operator can be used for string concatenation
            if(left is string || right is string) {
                return left.ToString() + right.ToString();
            }

            return (double)left + (double)right;
        }


        private bool IsNumber(object obj) {
            return obj is int || obj is double;
        }


        private bool IsEqual(object left, object right) {
            if(left == null && right == null) {
                return true;
            }

            return left == null ? false : left.Equals(right);
        }
    }
}
