namespace PrismAndMaui.MainApp;

using PrismAndMaui.MainApp.ViewModels;
using PrismAndMaui.MainApp.Views;

public class PrismAndMauiMainAppModule : IModule
{
    public void OnInitialized(IContainerProvider containerProvider)
    {

    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterInstance(new HttpClient
        {
            BaseAddress = new Uri("https://dansiegel.blob.core.windows.net")
        });
       
        containerRegistry
            .RegisterForNavigation<LoginPage, LoginViewModel>()
            .RegisterForNavigation<ContentRegionPage>()
            .RegisterForNavigation<RegionHome, RegionHomeViewModel>()
            .RegisterForNavigation<DefaultViewNamedPage>()
            .RegisterForNavigation<DefaultViewInstancePage>()
            .RegisterForNavigation<DefaultViewTypePage>()
            .RegisterForRegionNavigation<RegionViewA, RegionViewAViewModel>()
            .RegisterForRegionNavigation<RegionViewB, RegionViewBViewModel>()
            .RegisterForRegionNavigation<RegionViewC, RegionViewCViewModel>()
            .RegisterForNavigation<ViewA, ViewAViewModel>()
            .RegisterForNavigation<ViewB, ViewBViewModel>()
            .RegisterForNavigation<ViewC, ViewCViewModel>()
            .RegisterForNavigation<ViewD, ViewDViewModel>();
    }
}
