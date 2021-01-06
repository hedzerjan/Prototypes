using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CommandLine;
using Proto.Trials;

namespace Proto
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // if (args.Length == 0) args = new string[] { "parser", "-l", "DingDong", "--files", "file1.txt" };
            if (args.Length == 0) args = new string[] { "dependencyinjection"};
            Console.WriteLine($"Args: {string.Join(' ', args.Select(x => $"\"{x}\""))}");
            var types = LoadVerbs();
            await CommandLine.Parser.Default.ParseArguments(args, types)
                .WithParsedAsync(RunAsync);
                // .WithNotParsedAsync(HandleErrors);
        }

        static Type[] LoadVerbs()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
        }
        private static async Task RunAsync(object obj)
        {
            switch (obj)
            {
                case ParserOptions c:
                    ParserTrial.Run(c);
                    break;
                case AwaiterOptions c:
                    await AwaiterTrial.Run(c);
                    break;
                case DependencyInjectionOptions c:
                    DependencyInjectionTrial.Run(c);
                    break;
            }
        }
        private static void HandleErrors(IEnumerable<Error> obj)
        {
            foreach (var error in obj)
            {
                Console.WriteLine($"{error}");
            }
        }
    }

}
