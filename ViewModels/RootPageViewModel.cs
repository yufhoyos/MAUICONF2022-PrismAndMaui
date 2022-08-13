﻿namespace PrismAndMauiApp1.ViewModels;

using PrismAndMaui.MainApp.Events;

public class RootPageViewModel 
{
    private INavigationService _navigationService { get; }

    public RootPageViewModel(INavigationService navigationService)
    {
        
        _navigationService = navigationService;
        NavigateCommand = new DelegateCommand<string>(OnNavigateCommandExecuted);
        
    }

    public DelegateCommand<string> NavigateCommand { get; }

    private void OnNavigateCommandExecuted(string uri)
    {
        _navigationService.NavigateAsync(uri)
            .OnNavigationError(ex => Console.WriteLine(ex));
       
    }
}
