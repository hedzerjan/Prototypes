using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandLine;
using CommandLine.Text;
using Proto.Parser;

namespace Proto
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) args = new string[] { "parser", "-l" , "DingDong", "--files", "file1.txt"};
            Console.WriteLine($"Args: {string.Join(' ', args.Select(x => $"\"{x}\""))}");
            var types = LoadVerbs();
            CommandLine.Parser.Default.ParseArguments(args, types)
                .WithParsed(Run)
                .WithNotParsed(HandleErrors);
        }

        static Type[] LoadVerbs()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
        }
        private static void Run(object obj)
        {
            switch (obj)
            {
                case ParserOptions c:
                    ParserTrial.Run(c);
                    break;
            }
       }
        private static void HandleErrors(IEnumerable<Error> obj)
        {
            foreach(var error in obj)
            {
                Console.WriteLine($"{error}");
            }
        }
    }

}
