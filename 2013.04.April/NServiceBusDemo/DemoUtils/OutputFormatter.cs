using System;

namespace DemoUtils
{
    public class OutputFormatter : IDisposable
    {
        private readonly ConsoleColor _originalForegroundColor;

        public OutputFormatter(ConsoleColor color)
        {
            _originalForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
        }

        public void Dispose()
        {
            Console.ForegroundColor = _originalForegroundColor;
        }
    }
}
