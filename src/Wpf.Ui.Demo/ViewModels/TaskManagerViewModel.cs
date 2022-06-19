﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;

namespace Wpf.Ui.Demo.ViewModels;

public partial class TaskManagerViewModel : Wpf.Ui.Mvvm.ViewModelBase
{
    private bool _dataInitialized = false;

    [ObservableProperty]
    private ObservableCollection<INavigationControl> _navigationItems;

    [ObservableProperty]
    private ObservableCollection<INavigationControl> _navigationFooter;

    public TaskManagerViewModel()
    {
        if (!_dataInitialized)
            InitializeViewModel();
    }

    private void InitializeViewModel()
    {
        NavigationItems = new ObservableCollection<INavigationControl>
        {
            new NavigationItem
            {
                Content = "Processes",
                PageTag = "processes",
                Icon = SymbolRegular.Apps24,
                PageType = typeof(Views.Pages.TMPage)
            },
            new NavigationItem
            {
                Content = "Performance",
                PageTag = "processes",
                Icon = SymbolRegular.DesktopPulse24,
                PageType = typeof(Views.Pages.TMPage)
            },
            new NavigationItem
            {
                Content = "App history",
                PageTag = "app_history",
                Icon = SymbolRegular.History24,
                PageType = typeof(Views.Pages.TMPage)
            },
            new NavigationItem
            {
                Content = "Startup",
                PageTag = "startup",
                Icon = SymbolRegular.Home24,
                PageType = typeof(Views.Pages.TMPage)
            },
            new NavigationItem
            {
                Content = "Users",
                PageTag = "users",
                Icon = SymbolRegular.People24,
                PageType = typeof(Views.Pages.TMPage)
            },
            new NavigationItem
            {
                Content = "Details",
                PageTag = "details",
                Icon = SymbolRegular.TextBulletListLtr24,
                PageType = typeof(Views.Pages.TMPage)
            },
            new NavigationItem
            {
                Content = "Services",
                PageTag = "services",
                Icon = SymbolRegular.PuzzlePiece24,
                PageType = typeof(Views.Pages.TMPage)
            }
        };

        NavigationFooter = new ObservableCollection<INavigationControl>
        {
            new NavigationItem
            {
                Content = "Settings",
                PageTag = "settings",
                Icon = SymbolRegular.Settings24,
                PageType = typeof(Views.Pages.TMPage)
            }
        };

        _dataInitialized = true;
    }
}
