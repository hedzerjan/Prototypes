using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

[Verb("parser", HelpText = "Test all the options for parsing")]
public class ParserOptions
{
    [Option('l', "local", HelpText = "A flag that does nothing")]
    public bool Local { get; set; }
    [Value(0, MetaName = "NamelessString", HelpText = "A string that does not need a param name")]
    public string NamelessString { get; set; }
    // [Option('f', "files", HelpText = "A list of files")]
    [Option]
    public IEnumerable<string> Files { get; set; }
    [Usage(ApplicationAlias = "Proto")]
    public static IEnumerable<Example> Examples
    {
        get
        {
            yield return new Example("Normal scenario", new ParserOptions
            {
                Local = true,
                NamelessString = "MyNamelessString",
                Files = new string[] { "file1.txt", "file2.txt" }
            });
        }
    }
}