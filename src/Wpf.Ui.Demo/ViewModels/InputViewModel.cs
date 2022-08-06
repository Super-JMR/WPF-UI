﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using Wpf.Ui.Common.Interfaces;

namespace Wpf.Ui.Demo.ViewModels;

public partial class InputViewModel : ObservableObject, INavigationAware
{
    private bool _dataInitialized = false;

    [ObservableProperty]
    private IEnumerable<string> _autoSuggestCollection = new string[] { };

    [ObservableProperty]
    private IEnumerable<string> _comboCollection = new string[] { };

    public void OnNavigatedTo()
    {
        if (!_dataInitialized)
            InitializeData();
    }

    public void OnNavigatedFrom()
    {
        System.Diagnostics.Debug.WriteLine($"INFO | {typeof(InputViewModel)} navigated out", "Wpf.Ui.Demo");
    }

    private void InitializeData()
    {
        AutoSuggestCollection = new[]
        {
            "Blossoms",
            "Bloodmoss",
            "Blowbill",
            "Bryonia",
            "Buckthorn",
            "Celandine",
            "Cortinarius",
            "Crow's Eye",
            "Fools Parsley Leaves",
            "Ginatia Petals",
            "Han",
            "Hellebore Petals",
            "Honeysuckle",
            "Hop Umbels",
            "Hornwart",
            "Longrube",
            "Mandrake Root",
            "Moleyarrow",
            "Nostrix",
            "Pigskin Puffball",
            "Pringrape",
            "Ranogrin",
            "Ribleaf",
            "Sewant Mushrooms",
            "Verbena",
            "White Myrtle",
            "Wolfsbane"
        };

        ComboCollection = new[]
        {
            "Blossoms",
            "Bloodmoss",
            "Blowbill",
            "Bryonia",
            "Buckthorn",
            "Celandine",
            "Cortinarius",
            "Crow's Eye",
            "Fools Parsley Leaves",
            "Ginatia Petals",
            "Han",
        };

        _dataInitialized = true;
    }
}
