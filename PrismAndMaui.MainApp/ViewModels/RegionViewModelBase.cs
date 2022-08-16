﻿using Prism.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismAndMaui.MainApp.ViewModels;

public abstract class RegionViewModelBase : BindableBase, IRegionAware, IPageLifecycleAware
{
    protected string Name => GetType().Name.Replace("ViewModel", string.Empty);
    protected INavigationService _navigationService { get; }
    private IPageAccessor _pageAccessor { get; }
    protected IRegionNavigationService? _regionNavigation { get; private set; }

    protected RegionViewModelBase(INavigationService navigationService, IPageAccessor pageAccessor)
    {
        _navigationService = navigationService;
        _pageAccessor = pageAccessor;
    }

    public bool IsNavigationTarget(INavigationContext navigationContext) =>
        navigationContext.NavigatedName() == Name;

    private string? _message;
    public string? Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }

    private int _viewCount;
    public int ViewCount
    {
        get => _viewCount;
        set => SetProperty(ref _viewCount, value);
    }

    public string? PageName => _pageAccessor.Page?.GetType()?.Name;

    public void OnNavigatedFrom(INavigationContext navigationContext)
    {

    }

    public void OnNavigatedTo(INavigationContext navigationContext)
    {
        if (navigationContext.Parameters.ContainsKey(nameof(Message)))
            Message = navigationContext.Parameters.GetValue<string>(nameof(Message));

        _regionNavigation = navigationContext.NavigationService;
        ViewCount = navigationContext.NavigationService.Region.Views.Count();
    }

    public void OnAppearing()
    {
        RaisePropertyChanged(nameof(PageName));
    }

    public void OnDisappearing()
    {
    }
}