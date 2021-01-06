using System;
using Microsoft.Extensions.DependencyInjection;

namespace Proto.Trials
{
    public class DependencyInjectionTrial
    {
        public static void Run(DependencyInjectionOptions opts)
        {
            Console.WriteLine("Processing Verb 'DependencyInjection'");
            Console.WriteLine();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<ISomethingDoer, SomethingDoer>();
            serviceCollection.AddTransient<IManager, Manager>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var manager = serviceProvider.GetRequiredService<IManager>();
            manager.OrderSomething();
        }
    }

    public interface ISomethingDoer
    {
        public void DoSomething();
    }

    public class SomethingDoer : ISomethingDoer
    {
        public void DoSomething()
        {
            Console.WriteLine("Doing something!");
        }
    }

    public interface IManager
    {
        public void OrderSomething();
    }

    public class Manager : IManager
    {
        private readonly ISomethingDoer _somethingDoer;
        public Manager(ISomethingDoer somethingDoer)
        {
            _somethingDoer = somethingDoer;
        }
        public void OrderSomething()
        {
            _somethingDoer.DoSomething();
        }
    }
}