using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace Proto
{
    [Verb("dependencyinjection", HelpText = "Show the working of a simple DI example")]
    public class DependencyInjectionOptions
{
    [Usage(ApplicationAlias = "Proto")]
    public static IEnumerable<Example> Examples
    {
        get
        {
            yield return new Example("Normal scenario", new DependencyInjectionOptions
            {
            });
        }
    }
}}