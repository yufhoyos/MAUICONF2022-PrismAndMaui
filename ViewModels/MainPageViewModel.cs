namespace PrismAndMauiApp1.ViewModels;

using PrismAndMaui.MainApp.Events;

internal class MainPageViewModel : BindableBase, INavigatedAware
{
    private INavigationService _navigationService { get; }
    private IEventAggregator _ea { get; }
    private DemoEvent evento;
    private string _MensajeEvento;
    public string MensajeEvento
    {
        get { return _MensajeEvento; }
        set { SetProperty(ref _MensajeEvento, value); }
    }

    public MainPageViewModel(INavigationService navigationService, IEventAggregator ea)
    {
        _ea = ea;
        _navigationService = navigationService;
        NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted);
        evento = _ea.GetEvent<DemoEvent>();
    }

    public DelegateCommand<string> NavigateCommand { get; }

    private void OnNavigateCommandExecuted(string uri)
    {
        _navigationService.NavigateAsync(uri)
            .OnNavigationError(ex => Console.WriteLine(ex));
    }

    public void OnNavigatedFrom(INavigationParameters parameters)
    {
        evento.Unsubscribe(HandleDemoEvent);
    }

    public void OnNavigatedTo(INavigationParameters parameters)
    {
        evento.Subscribe(HandleDemoEvent);
    }

    void HandleDemoEvent(string mensaje)
    {
        MensajeEvento = mensaje;
    }
}
