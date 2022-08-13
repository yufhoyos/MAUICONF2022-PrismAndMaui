﻿namespace PrismAndMauiApp1;

using CommunityToolkit.Maui;
using PrismAndMauiApp1.ViewModels;


public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UsePrismApp<App>(PrismStartup.Configure)
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }


}
