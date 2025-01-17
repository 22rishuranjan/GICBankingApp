public class BaseTest : IDisposable
{
    private readonly TextReader _originalConsoleIn;

    public BaseTest()
    {
        _originalConsoleIn = Console.In;
    }

    public void Dispose()
    {
        Console.SetIn(_originalConsoleIn);
    }
}
