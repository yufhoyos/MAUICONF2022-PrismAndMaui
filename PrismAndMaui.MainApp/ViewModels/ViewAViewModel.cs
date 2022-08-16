using PrismAndMaui.MainApp.Events;

namespace PrismAndMaui.MainApp.ViewModels;

public class ViewAViewModel : ViewModelBase
{
    public DelegateCommand PubEventCommand { get; set; }
    private string _MensajeEvento;
    public string MensajeEvento
    {
        get { return _MensajeEvento; }
        set { SetProperty(ref _MensajeEvento, value); }
    }
    public ViewAViewModel(BaseServices baseServices)
        : base(baseServices)
    {
        PubEventCommand = new DelegateCommand(OnPubEvent);
    }

    public void OnPubEvent()
    {
        EventAggregator.GetEvent<DemoEvent>().Publish(MensajeEvento);
    }
}
