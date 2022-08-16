using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismAndMauiApp1.ViewModels
{
    public class SplashPageViewModel : IPageLifecycleAware
    {
        private INavigationService _navigationService { get; }

        public SplashPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnAppearing()
        {
            _navigationService.CreateBuilder()
                .UseAbsoluteNavigation()
                .AddSegment<RootPageViewModel>()
                .Navigate();
        }

        public void OnDisappearing()
        {

        }

    }
}
