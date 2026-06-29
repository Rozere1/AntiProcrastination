using Microsoft.Extensions.Logging;

public class Logger : ILogger 
{
    IDisposable? ILogger.BeginScope<TState>(TState state)
    {
        throw new NotImplementedException();
    }

    bool ILogger.IsEnabled(LogLevel logLevel)
    {
        throw new NotImplementedException();
    }

    void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        throw new NotImplementedException();
    }
}