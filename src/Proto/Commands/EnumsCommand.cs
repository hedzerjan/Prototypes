using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Proto.Commands
{
    public class EnumsSettings : CommandSettings
    { }

    public class EnumsCommand : AsyncCommand<EnumsSettings>
    {
        private ILogger Logger { get; }

        public EnumsCommand(ILogger<ConsoleCommand> logger)
        {
            Logger = logger;
        }

        public override Task<int> ExecuteAsync(CommandContext context, EnumsSettings settings)
        {
            Logger.LogInformation($"Starting enums");
            Dinges a = Dinges.een;
            Logger.LogInformation($"{a}");
            Logger.LogInformation($"{Enum.ToObject(typeof(Dinges), 2)}");
            Logger.LogInformation(String.Join(',', Enum.GetNames(typeof(Dinges))));

            StackingDebuff debuff = StackingDebuff.Slow;
            Logger.LogInformation($"debuff: {debuff}");
            debuff |= StackingDebuff.Confused;
            Logger.LogInformation($"debuff after |=: {debuff}");
            return Task.FromResult<int>(0);
        }
        enum Dinges : int
        {
            een = 1,
            twee = 2
        }

        [Flags]
        public enum StackingDebuff : UInt64
        {
            None = 0,
            Bleeding = 1 << 1,
            Poison = 1 << 2,
            Slow = 1 << 3,
            Confused = 1 << 4,
            All = Bleeding | Poison | Slow | Confused
        }
    }
}
