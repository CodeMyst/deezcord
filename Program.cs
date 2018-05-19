using System;
using System.IO;

using Microsoft.Extensions.Configuration;

using Avalonia;
using Avalonia.Logging.Serilog;

using DeezCord.Views;
using DeezCord.ViewModels;

namespace DeezCord
{
    public static class Program
    {
        private static void Main ()
        {
            BuildAvaloniaApp().Start<MainWindow>(() => new MainWindowViewModel ());
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI ()
                .LogToDebug();
    }
}
