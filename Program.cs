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
        public static IConfiguration Configuration { get; set; }

        private static void Main ()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder ().SetBasePath (Directory.GetCurrentDirectory ())
                                                     .AddJsonFile ("appsettings.json");

            Configuration = builder.Build ();

            BuildAvaloniaApp().Start<MainWindow>(() => new MainWindowViewModel ());
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI ()
                .LogToDebug();
    }
}
