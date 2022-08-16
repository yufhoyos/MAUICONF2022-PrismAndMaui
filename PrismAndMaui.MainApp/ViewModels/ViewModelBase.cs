using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrismAndMaui.MainApp.ViewModels
{
    public abstract class ViewModelBase : BindableBase, IInitialize, INavigatedAware, IPageLifecycleAware
    {
        private readonly BaseServices _baseServices;

        protected ViewModelBase(BaseServices baseServices)
        {
            _baseServices = baseServices;
            Title = Regex.Replace(GetType().Name, "ViewModel", string.Empty);
            Id = Guid.NewGuid().ToString();
            NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted);
            ShowPageDialog = new DelegateCommand(OnShowPageDialog);
            Messages = new ObservableCollection<string>();
            Messages.CollectionChanged += (sender, args) =>
            {
                foreach (string message in args.NewItems)
                    Console.WriteLine($"{Title} - {message}");
            };
            ShowDialog = new DelegateCommand(OnShowDialogCommand, () => !string.IsNullOrEmpty(SelectedDialog))
                .ObservesProperty(() => SelectedDialog);
        }

        protected INavigationService NavigationService => _baseServices.NavigationService;

        protected IRegionManager RegionManager => _baseServices.RegionManager;

        protected IPageDialogService PageDialogs => _baseServices.PageDialogs;

        protected IEventAggregator EventAggregator => _baseServices.EventAggregator;


        private string? _title;
        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public string Id { get; }

        private string _selectedDialog;
        public string SelectedDialog
        {
            get => _selectedDialog;
            set => SetProperty(ref _selectedDialog, value);
        }

        public ObservableCollection<string> Messages { get; }

        public DelegateCommand<string> NavigateCommand { get; }

        public DelegateCommand ShowPageDialog { get; }

        public DelegateCommand ShowDialog { get; }

        private void OnNavigateCommandExecuted(string uri)
        {
            Messages.Add($"OnNavigateCommandExecuted: {uri}");
            NavigationService.NavigateAsync(uri)
                .OnNavigationError(ex => Console.WriteLine(ex));
        }

        private void OnShowPageDialog()
        {
            Messages.Add("OnShowPageDialog");
            PageDialogs.DisplayAlertAsync("Message", $"Hello from {Title}. This is a Page Dialog Service Alert!", "Ok");
        }

        private void OnShowDialogCommand()
        {
            Messages.Add("OnShowDialog");
            PageDialogs.DisplayAlertAsync("Message", $"Hello from {Title}. This is a Page Dialog Service Alert!", "Ok");
        }

        public void Initialize(INavigationParameters parameters)
        {
            Messages.Add("ViewModel Initialized");
            foreach (var parameter in parameters.Where(x => x.Key.Contains("message")))
                Messages.Add(parameter.Value.ToString());
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            Messages.Add("ViewModel NavigatedFrom");
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Messages.Add("ViewModel NavigatedTo");
        }

        public void OnAppearing()
        {
            Messages.Add("View Appearing");
        }

        public void OnDisappearing()
        {
            Messages.Add("View Disappearing");
        }
    }
}
