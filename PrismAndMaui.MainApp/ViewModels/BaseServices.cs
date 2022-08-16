namespace PrismAndMaui.MainApp.ViewModels;

using Prism.Events;
using Prism.Services.Dialogs;

public class BaseServices
{
    public BaseServices(
        INavigationService navigationService,
         IRegionManager regionManager,
        IPageDialogService pageDialogs,
         IEventAggregator eventAggregator)
    {
        NavigationService = navigationService;
        RegionManager = regionManager;
        PageDialogs = pageDialogs;
        EventAggregator = eventAggregator;
    }

    public INavigationService NavigationService { get; }
    public IRegionManager RegionManager { get; }
    public IPageDialogService PageDialogs { get; }
    public IEventAggregator EventAggregator { get; }
}
