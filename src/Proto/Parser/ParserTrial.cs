using System;

namespace Proto.Parser
{
    public class ParserTrial
    {
        public static void Run(ParserOptions opts)
        {
            Console.WriteLine("Processing Verb 'Parser'");
            Console.WriteLine($"opts.Local: {opts.Local}");
            Console.WriteLine($"opts.NamelessString: {opts.NamelessString}");
            Console.WriteLine($"opts.Files: {string.Join(',', opts.Files)}");
            Console.WriteLine();
        }
    }
}