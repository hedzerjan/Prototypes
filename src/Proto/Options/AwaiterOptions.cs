using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace Proto
{
    [Verb("awaiter", HelpText = "Test all the options for parsing")]
    public class AwaiterOptions
{
    [Usage(ApplicationAlias = "Proto")]
    public static IEnumerable<Example> Examples
    {
        get
        {
            yield return new Example("Normal scenario", new AwaiterOptions
            {
            });
        }
    }
}}