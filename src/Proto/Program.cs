using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Proto.Commands;
using Spectre.Console.Cli;
using Spectre.Cli.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection()
    .AddLogging(configure =>
            configure
                .AddSimpleConsole(opts =>
                {
                    opts.TimestampFormat = "HH:mm:ss:fff ";
                })
    );

using var registrar = new DependencyInjectionRegistrar(serviceCollection);
var app = new CommandApp(registrar);

app.Configure(
    config =>
    {
        config.ValidateExamples();

        config.AddCommand<ConsoleCommand>("console")
                .WithDescription("Example console command.")
                .WithExample(new[] { "console" });
        config.AddCommand<AwaiterCommand>("awaiter")
                .WithDescription("Show what happens in a state machine.")
                .WithExample(new[] { "awaiter" });
        config.AddCommand<EventsCommand>("events")
               .WithDescription("Show two different ways to handle events")
               .WithExample(new[] { "events" });
        config.AddCommand<EnumsCommand>("enums")
                 .WithDescription("Show some examples with enumbs")
                 .WithExample(new[] { "enums" });

    });

return await app.RunAsync(args);