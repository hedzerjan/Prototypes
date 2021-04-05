using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Spectre.Console.Cli;

namespace Proto.Commands
{
#nullable disable
    public class EventsSettings : CommandSettings
    {
        [CommandArgument(0, "<type>")]
        [Description("The type of events, 'simple' or 'ms'")]
        public string Type { get; set; } = string.Empty;
    }

    public class EventsCommand : AsyncCommand<EventsSettings>
    {
        private ILogger Logger { get; }

        public EventsCommand(ILogger<ConsoleCommand> logger)
        {
            Logger = logger;
        }

        public override async Task<int> ExecuteAsync(CommandContext context, EventsSettings settings)
        {
            Logger.LogInformation("Starting events test");
            switch (settings.Type)
            {
                case "ms":
                    TestMSEvent();
                    break;
                case "simple":
                    TestSimpleEvent();
                    break;
            }
            return await Task.FromResult<int>(0);
        }
        private void TestMSEvent()
        {
            var pub = new MSPublisher();
            _ = new MSSubscriber("sub1", pub);
            _ = new MSSubscriber("sub2", pub);
            Logger.LogInformation("Publisher created");
            pub.DoSomething();
        }

        private void TestSimpleEvent()
        {
            var pubOne = new Publisher
            {
                Name = "one"
            };
            var pubTwo = new Publisher
            {
                Name = "two"
            };
            var sub = new Subscriber();

            pubOne.Publish();
            pubTwo.Publish();
        }

        public static ILogger GetLogger(string name)
        {
            using var factory = LoggerFactory.Create(b =>
            {
                b.SetMinimumLevel(LogLevel.Trace).AddSimpleConsole(opts =>
                    opts.TimestampFormat = "HH:mm:ss:fff "
                );
            });
            return factory.CreateLogger(name);
        }
    }
    public class CustomEventArgs : EventArgs
    {
        public CustomEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }

    class MSPublisher
    {
        public event EventHandler<CustomEventArgs> RaiseCustomEvent;

        public void DoSomething()
        {
            OnRaiseCustomEvent(new CustomEventArgs("Event triggered"));
        }

        protected virtual void OnRaiseCustomEvent(CustomEventArgs e)
        {
            EventHandler<CustomEventArgs> raiseEvent = RaiseCustomEvent;

            if (raiseEvent != null)
            {
                e.Message += $" at {DateTime.Now}";
                raiseEvent(this, e);
            }
        }
    }

    class MSSubscriber
    {
        private readonly string _id;
        private readonly ILogger logger;

        public MSSubscriber(string id, MSPublisher pub)
        {
            _id = id;
            logger = EventsCommand.GetLogger("MSSubscriber");
            // Subscribe to the event
            pub.RaiseCustomEvent += HandleCustomEvent;
        }

        // Define what actions to take when the event is raised.
        void HandleCustomEvent(object sender, CustomEventArgs e)
        {
            logger.LogInformation($"{_id} received this message: {e.Message}");
        }
    }

    class Publisher
    {
        public static event Action<string> OnString;
        public string Name { get; set; } = "";
        public void Publish()
        {
            OnString?.Invoke(Name);
        }
    }

    class Subscriber
    {
        private readonly ILogger logger;

        public Subscriber()
        {
            Publisher.OnString += GetTheName;
            logger = EventsCommand.GetLogger("Subscriber");
        }

        private void GetTheName(string obj)
        {
            logger.LogInformation(obj);
        }
    }
}
