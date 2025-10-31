using Xunit;
using AsyncTaskRunner;

namespace async_task_runner.Tests;

public class UnitTest_Callables {
    [Fact]
    public void add_callables_to_same_container() {
        // addition function test
        var add_test = new Callable<int>(
            args => {
                int a = (int)args[0];
                int b = (int)args[1];
                return a + b;
            },
            expectedArgs: 2
        );

        // subtraction function test
        var sub_test = new Callable<int>(
            args => {
                int a = (int)args[0];
                int b = (int)args[1];
                return a - b;
            },
            expectedArgs: 2
        );

        var callables = new List<ICallable>{};
        // Add them to the list
        callables.Add(add_test);
        callables.Add(sub_test);

        // Assert the list contains them
        Assert.Contains(add_test, callables);
        Assert.Contains(sub_test, callables);

        // Optional: assert count
        Assert.Equal(2, callables.Count);
    }

    [Fact]
    public void correct_number_of_args_in_callable() {
        // addition function test
        var add_test = new Callable<int>(
            args => {
                int a = (int)args[0];
                int b = (int)args[1];
                return a + b;
            },
            expectedArgs: 2
        );

        // checks if ArgumentException is thrown if arguements count is not equal (as specified with expectedArgs)
        // 3 args here, 2 args expected     
        Assert.Throws<ArgumentException>(() => add_test.Invoke(10, 4, "message"));
    }
}
