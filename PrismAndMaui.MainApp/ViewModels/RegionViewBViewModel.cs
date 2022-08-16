namespace PrismAndMaui.MainApp.ViewModels;

using Prism.Common;
using PrismAndMaui.MainApp.Events;

public class RegionViewBViewModel : RegionViewModelBase
{
    IEventAggregator EventAggregator;
    public DelegateCommand PubEventCommand { get; set; }
    private string _MensajeEvento;
    public string MensajeEvento
    {
        get { return _MensajeEvento; }
        set { SetProperty(ref _MensajeEvento, value); }
    }
    public RegionViewBViewModel(INavigationService navigationService, IPageAccessor pageAccessor, IEventAggregator eventAggregator)
        : base(navigationService, pageAccessor)
    {
        PubEventCommand = new DelegateCommand(OnPubEvent);
        EventAggregator = eventAggregator;
    }
    public void OnPubEvent()
    {
        EventAggregator.GetEvent<DemoEvent>().Publish(MensajeEvento);
    }
}
