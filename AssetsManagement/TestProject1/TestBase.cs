using Xunit.Abstractions;

namespace TestProject1;

public abstract class TestBase
{
    private readonly ITestOutputHelper _output;

    protected TestBase(ITestOutputHelper output)
    {
        _output = output;
    }

    protected void Log(string message)
    {
        _output.WriteLine(message);
    }
}