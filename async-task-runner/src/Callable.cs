using System;

namespace AsyncTaskRunner {

    public interface ICallable {
        object? Invoke(params object[] args);
    }

    // T is return type only, arguements can be any type, and can have as many as needed
    public class Callable<T> : ICallable {
        private readonly Func<object[], T> _func;
        private readonly int _expectedArgs;     // makes sure that the number of arguements is consistent

        public Callable(Func<object[], T> func, int expectedArgs = -1) {
            _func = func;
            _expectedArgs = expectedArgs;
        }

        public object Invoke(params object[] args) {
            if (_expectedArgs >= 0 && args.Length != _expectedArgs) {
                throw new ArgumentException($"Expected {_expectedArgs} arguments, got {args.Length}");
            }
            return _func(args)!;
        }
    }
}