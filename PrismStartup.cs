namespace PrismAndMauiApp1;

using PrismAndMaui.MainApp.ViewModels;
using PrismAndMauiApp1.Pages;
using PrismAndMauiApp1.ViewModels;
using PrismAndMauiApp1.Views;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
        builder.ConfigureModuleCatalog(moduleCatalog =>
        {
            moduleCatalog.AddModule<PrismAndMaui.MainApp.PrismAndMauiMainAppModule>();
        });
        builder.RegisterTypes(RegisterTypes);
        builder.OnAppStart(navigationService =>
                    navigationService.CreateBuilder()
                    .AddSegment<RootPageViewModel>()
                    .Navigate(HandleNavigationError));
    }

    private static void HandleNavigationError(Exception ex)
    {
        Console.WriteLine(ex);
        System.Diagnostics.Debugger.Break();
    }

    private static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterScoped<BaseServices>();
        containerRegistry.RegisterForNavigation<MainPage>()
                         .RegisterForNavigation<RootPage>()
                         .RegisterForNavigation<SplashPage>()
                         .RegisterInstance(DeviceInfo.Current)
                         .RegisterInstance(SemanticScreenReader.Default)
                         .RegisterInstance(Launcher.Default);
    }
}
