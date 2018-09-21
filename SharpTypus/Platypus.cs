using System;
using System.IO;
using SharpTypus.Interpreting;
using SharpTypus.Parsing;
using SharpTypus.Parsing.Expressions;

namespace SharpTypus {
    class Platypus {
        private static bool exceptionEncountered = false;
        private static bool runtimeExceptionEncountered = false;


        public static void Main(string[] args) {
            if(args.Length > 1) {
                Console.WriteLine("Usage: platypus [script]");
                return;
            }

            if(args.Length == 1) {
                RunScript(args[0]);
            }
            else {
                RunPrompt();
            }
        }

        public static void GenerateException(int line, string message) {
            ReportException(line, message);
            exceptionEncountered = true;
        }

        public static void GenerateException(Token token, string message) {
            ReportException(token.Line, message);
            exceptionEncountered = true;
        }

        public static void GenerateRuntimeException(RuntimeException exception) {
            ReportRuntimeError(exception.Message);
        }


        private static void RunPrompt() {
            while(true) {
                Console.Write("> ");
                Run(Console.ReadLine());
                exceptionEncountered = false;
            }
        }

        private static void RunScript(string scriptName) {
            string[] script;
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"{scriptName}");
            try {
                script = System.IO.File.ReadAllLines(path);
            }
            catch(System.IO.FileNotFoundException) {
                Console.WriteLine($"Error while opening script.");
                return;
            }

            Run(string.Join('\n', script));
            
            if(exceptionEncountered || runtimeExceptionEncountered) {
                return;
            }
        }

        private static void Run(String source) {
            var lexer = new Lexer(source);
            var tokens = lexer.Tokenize();

            var parser = new Parser(tokens);
            var printer = new AstPrinter();
            var statements = parser.Parse();
            printer.Print(statements);

            var interpreter = new Interpreter();
            interpreter.Interpret(statements);
        }

        private static void ReportException(int line, string message) {
            Console.WriteLine($"Exception: line {line}, {message}");
        }

        private static void ReportRuntimeError(string message) {
            Console.WriteLine($"Runtime exception: {message}");
        }

    }
}

