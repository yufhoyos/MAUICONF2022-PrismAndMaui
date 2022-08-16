namespace PrismAndMaui.MainApp.ViewModels;



public class LoginViewModel : ViewModelBase
{
    
    public LoginViewModel(BaseServices baseServices)
        : base(baseServices)
    {
        Title = "Inicio";
        LoginCommand = new DelegateCommand(OnLoginCommandExecuted, () => !IsBusy)
           .ObservesProperty(() => IsBusy);
    }

    public string NameTitle => "Cual es tu Nombre?";

    private string _name;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public DelegateCommand LoginCommand { get; }

    private async void OnLoginCommandExecuted()
    {
        IsBusy = true;
        try
        {
            await NavigationService.CreateBuilder()
                 .UseAbsoluteNavigation()
                 .AddSegment("SplashPage")
                 .AddParameter("authenticated", true)
                 .NavigateAsync();
        }
        finally
        {
            IsBusy = false;
        }
    }
}