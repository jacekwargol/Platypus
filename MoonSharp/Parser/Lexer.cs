using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static MoonSharp.Parser.TokenType;

namespace MoonSharp.Parser {
    class Lexer {
        private readonly string source;
        private readonly List<Token> tokens = new List<Token>();

        private int start, current;
        private int line = 1;

        private static Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>() {
            { "and", And},
            { "or", Or },
            { "if", If },
            { "else", Else },
            { "elif", Elif },
            { "true", True },
            { "false", False },
            { "class", Class },
            { "fun", Fun },
            { "for", For },
            { "this", This },
            { "base", Base },
            { "let", Let },
            { "return", Return }
        };

        public Lexer(string source) => this.source = source;

        public List<Token> Tokenize() {
            while(current < source.Length) {
                start = current;
                ScanToken();
            }
            return tokens;
        }

        private void ScanToken() {
            var c = source[current++];

            switch(c) {
                case '(': AddToken(LeftParen); break;
                case ')': AddToken(RightParen); break;
                case '{': AddToken(LeftBracket); break;
                case '}': AddToken(RightBracket); break;
                case ',': AddToken(Comma); break;
                case '.': AddToken(Dot); break;
                case ';': AddToken(Semicolon); break;
                case ':': AddToken(Colon); break;
                case '+': AddToken(Plus); break;
                case '-': AddToken(Minus); break;
                case '*': AddToken(Star); break;

                case '=':
                    AddToken(IsMatching('=') ? EqualEqual : Equal);
                    break;
                case '!':
                    AddToken(IsMatching('=') ? BangEqual : Bang);
                    break;
                case '<':
                    AddToken(IsMatching('<') ? LessEqual : Less);
                    break;
                case '>':
                    AddToken(IsMatching('>') ? GreaterEqual : Greater);
                    break;

                case '/':
                    if(!IsMatching('/')) {
                        AddToken(Slash);
                        break;
                    }
                    // Ignore one-line comments
                    while(current < source.Length && source[current] != '\n') {
                        current++;
                    }
                    break;

                case '"':
                    TryString();
                    break;

                // TODO: match float starting with dot
                case var num when new Regex(@"[0-9]").IsMatch(c.ToString()):
                    TryNumber();
                    break;

                case var iden when new Regex(@"[a-zA-Z_]").IsMatch(c.ToString()):
                    TryIdentifier();
                    break;

                // Ignore whitespace
                case '\r':
                case '\t':
                case ' ':
                    break;

                case '\n':
                    line++;
                    break;

                default:
                    Moon.GenerateError(line, "Unexpected character.");
                    break;
            }
        }

        private void TryIdentifier() {
            var idenRegex = new Regex(@"[a-zA-Z_\?0-9]");
            while(!IsAtEnd() && idenRegex.IsMatch(source[current].ToString())) {
                current++;
            }

            var lex = source.Substring(start, current - start);
            foreach(var iden in keywords) {
                if(lex == iden.Key) {
                    AddToken(iden.Value, lex);
                    return;
                }
            }

            AddToken(Identifier, lex);
        }

        private void TryNumber() {
            int dotCount = 0;
            var floatRegex = new Regex(@"[0-9\.]");
            while(!IsAtEnd() && floatRegex.IsMatch(source[current].ToString())) {
                if(source[current] == '.') {
                    dotCount++;
                }

                current++;
            }

            if(dotCount > 1) {
                Moon.GenerateError(line, "Unexpected character.");
                return;
            }

            AddToken(dotCount == 1 ? Float : Integer);
        }

        private void AddToken(TokenType type, string lexame = null) {
            var lex = lexame ?? source.Substring(start, current - start);
            var token = new Token(type, lex, line);
            tokens.Add(token);
        }

        private void TryString() {
            while(!IsAtEnd() && source[current] != '\n' && source[current] != '"') {
                current++;
            }

            if(!IsAtEnd() && source[current] == '"') {
                current++;
                var lexame = source.Substring(start, current - start);
                AddToken(StringToken, lexame);
                return;
            }

            Moon.GenerateError(line, "String does not have closing \"");
        }

        private bool IsAtEnd() => current >= source.Length;

        private bool IsMatching(char expected) {
            if(IsAtEnd()) {
                return false;
            }

            if(source[current] != expected) {
                return false;
            }

            current++;
            return true;
        }
    }
}
