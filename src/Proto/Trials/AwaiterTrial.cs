using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Proto.Trials
{
    public static class AwaiterTrial
    {
        private static bool _returnCompletedTask;

        public static async Task Run(AwaiterOptions opts)
        {
            Console.WriteLine("Before first await");
            _returnCompletedTask = true;
            var res1 = await new MyAwaitable();
            Console.WriteLine(res1);

            Console.WriteLine("Before second await");
            _returnCompletedTask = false;
            var res2 = await new MyAwaitable();
            Console.WriteLine(res2);
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
                    Console.WriteLine("IsCompleted called.");
                    return _returnCompletedTask;
                }
            }
            public int GetResult()
            {
                Console.WriteLine("GetResult called.");
                return 5;
            }
            public void OnCompleted(Action continuation)
            {
                Console.WriteLine("OnCompleted called.");
                continuation();
            }
        }
    }
}