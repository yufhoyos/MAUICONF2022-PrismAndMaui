namespace PrismAndMaui.MainApp.ViewModels;

using Prism.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RegionViewAViewModel : RegionViewModelBase, IInitialize
{
    public RegionViewAViewModel(INavigationService navigationService, IPageAccessor pageAccessor)
        : base(navigationService, pageAccessor)
    {
    }

    public void Initialize(INavigationParameters parameters)
    {
        if (parameters.TryGetValue<string>("Message", out var message))
            Message = message;
    }
}
