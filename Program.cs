using System; 

namespace AsyncTaskRunner {

    public interface ICallable {
        object? Invoke(params object[] args);
    }

    public class Callable<T> : ICallable {
        private readonly Func<object[], T> _func;
        private readonly int _expectedArgs;

        public Callable(Func<object[], T> func, int expectedArgs = -1) {
            _func = func;
            _expectedArgs = expectedArgs;
        }

        public object Invoke(params object[] args) {
            if (_expectedArgs >= 0 && args.Length != _expectedArgs)
                throw new ArgumentException($"Expected {_expectedArgs} arguments, got {args.Length}");

            return _func(args)!;
        }
    }
    
    class TaskRunner {

        static void Main(string[] args) {
            var add_test = new Callable<int>(
                args => {
                    int a = (int)args[0];
                    int b = (int)args[1];
                    return a + b;
                },
                expectedArgs: 2
            );

            var sub_test = new Callable<int>(
                args => {
                    int a = (int)args[0];
                    int b = (int)args[1];
                    return a - b;
                },
                expectedArgs: 2
            );

            var callables = new List<ICallable> { add_test, sub_test };

            Console.WriteLine(callables[0].Invoke(10, 4));
            Console.WriteLine(callables[1].Invoke(10, 4));
        } 
    } 
}