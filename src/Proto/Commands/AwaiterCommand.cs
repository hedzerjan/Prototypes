using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Proto.Commands
{
    public class AwaiterSettings : CommandSettings
    { }

    public class AwaiterCommand : AsyncCommand<AwaiterSettings>
    {
        private static bool _returnCompletedTask;
        private ILogger Logger { get; }

        public AwaiterCommand(ILogger<ConsoleCommand> logger)
        {
            Logger = logger;
        }

        public override async Task<int> ExecuteAsync(CommandContext context, AwaiterSettings settings)
        {
            Logger.LogInformation("Before first await");
            _returnCompletedTask = true;
            var res1 = await new MyAwaitable();
            Logger.LogInformation(res1.ToString());

            Logger.LogInformation("Before second await");
            _returnCompletedTask = false;
            var res2 = await new MyAwaitable();
            Logger.LogInformation(res2.ToString());

            return await Task.FromResult<int>(0);
        }

        public class MyAwaitable
        {
            public MyAwaiter GetAwaiter() => new MyAwaiter();
        }
        public class MyAwaiter : INotifyCompletion
        {
            public bool IsCompleted
            {
                get
                {
                    AnsiConsole.MarkupLine("\t[underline aqua]IsCompleted[/] called.");
                    return _returnCompletedTask;
                }
            }

            public int GetResult()
            {
                AnsiConsole.MarkupLine("\t[underline aqua]GetResult[/] called.");
                return 5;
            }
            public void OnCompleted(Action continuation)
            {
                AnsiConsole.MarkupLine("\t[underline aqua]OnCompleted[/] called.");
                continuation();
            }
        }
    }
}
