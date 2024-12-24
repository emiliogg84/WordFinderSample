using System.Runtime.CompilerServices;

namespace WordFinderApp.Helpers;

public static class ExceptionHelper
{
    public static void ThrowIfNull(object argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null) throw new ArgumentNullException(paramName);
    }

    public static void ThrowIfTrue(bool expression, string message)
    {
        if (expression) throw new ArgumentException(message);
    }
}
