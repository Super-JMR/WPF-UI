﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

#nullable enable

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Wpf.Ui.Appearance;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Demo.Models.Colors;

namespace Wpf.Ui.Demo.ViewModels;

public partial class ColorsViewModel : Wpf.Ui.Mvvm.ViewModelBase, INavigationAware
{
    private bool _dataInitialized = false;

    private readonly string[] _paletteResources =
   {
        "PalettePrimaryBrush",
        "PaletteRedBrush",
        "PalettePinkBrush",
        "PalettePurpleBrush",
        "PaletteDeepPurpleBrush",
        "PaletteIndigoBrush",
        "PaletteBlueBrush",
        "PaletteLightBlueBrush",
        "PaletteCyanBrush",
        "PaletteTealBrush",
        "PaletteGreenBrush",
        "PaletteLightGreenBrush",
        "PaletteLimeBrush",
        "PaletteYellowBrush",
        "PaletteAmberBrush",
        "PaletteOrangeBrush",
        "PaletteDeepOrangeBrush",
        "PaletteBrownBrush",
        "PaletteGreyBrush",
        "PaletteBlueGreyBrush"
    };

    private readonly string[] _themeResources =
    {
        "SystemAccentColorPrimaryBrush",
        "SystemAccentColorSecondaryBrush",
        "SystemAccentColorTertiaryBrush",

        "ControlElevationBorderBrush",
        "CircleElevationBorderBrush",

        "AccentControlElevationBorderBrush",

        "TextFillColorPrimaryBrush",
        "TextFillColorSecondaryBrush",
        "TextFillColorTertiaryBrush",
        "TextFillColorDisabledBrush",
        "TextFillColorInverseBrush",

        "AccentTextFillColorDisabledBrush",
        "TextOnAccentFillColorSelectedTextBrush",

        "ControlFillColorDefaultBrush",
        "ControlFillColorSecondaryBrush",
        "ControlFillColorTertiaryBrush",
        "ControlFillColorDisabledBrush",
        "ControlSolidFillColorDefaultBrush",
        "AccentFillColorDisabledBrush",
        "MenuBorderColorDefaultBrush",

        "SystemFillColorSuccessBrush",
        "SystemFillColorCautionBrush",
        "SystemFillColorCriticalBrush",
        "SystemFillColorNeutralBrush",
        "SystemFillColorSolidNeutralBrush",
        "SystemFillColorAttentionBackgroundBrush",
        "SystemFillColorSuccessBackgroundBrush",
        "SystemFillColorCautionBackgroundBrush",
        "SystemFillColorCriticalBackgroundBrush",
        "SystemFillColorNeutralBackgroundBrush",
        "SystemFillColorSolidAttentionBackgroundBrush",
        "SystemFillColorSolidNeutralBackgroundBrush"
    };

    [ObservableProperty]
    private IEnumerable<Pa__one> _paletteBrushes;

    [ObservableProperty]
    private IEnumerable<Pa__one> _themeBrushes;

    [ObservableProperty]
    private int _columns;

    public ColorsViewModel()
    {
        Wpf.Ui.Appearance.Theme.Changed += ThemeOnChanged;
        _paletteBrushes = Array.Empty<Pa__one>();
        _themeBrushes = Array.Empty<Pa__one>();
    }

    /// <inheritdoc />
    protected override void OnViewCommand(object? parameter = null)
    {
    }

    public void OnNavigatedTo()
    {
        if (!_dataInitialized)
            InitializeData();
    }

    public void OnNavigatedFrom()
    {
    }

    private void ThemeOnChanged(ThemeType currentTheme, Color systemAccent)
    {
        FillTheme();
    }

    private void InitializeData()
    {
        _dataInitialized = true;

        FillPalette();
        FillTheme();
    }

    private void FillPalette()
    {
        var pallete = new List<Pa__one> { };

        foreach (var singleBrushKey in _paletteResources)
        {
            var singleBrush = Application.Current.Resources[singleBrushKey] as Brush;

            if (singleBrush == null)
                continue;

            string description;

            if (singleBrush is SolidColorBrush solidColorBrush)
                description =
                    $"R: {solidColorBrush.Color.R}, G: {solidColorBrush.Color.G}, B: {solidColorBrush.Color.B}";
            else
                description = "Gradient";

            pallete.Add(new Pa__one
            {
                Title = "PALETTE",
                Subtitle = description + "\n" + singleBrushKey,
                Brush = singleBrush,
                BrushKey = singleBrushKey
            });
        }

        PaletteBrushes = pallete;
    }

    private void FillTheme()
    {
        var theme = new List<Pa__one> { };

        foreach (var singleBrushKey in _themeResources)
        {
            var singleBrush = Application.Current.Resources[singleBrushKey] as Brush;

            if (singleBrush == null)
                continue;

            string description;

            if (singleBrush is SolidColorBrush solidColorBrush)
                description =
                    $"R: {solidColorBrush.Color.R}, G: {solidColorBrush.Color.G}, B: {solidColorBrush.Color.B}";
            else
                description = "Gradient";

            theme.Add(new Pa__one
            {
                Title = "THEME",
                Subtitle = description + "\n" + singleBrushKey,
                Brush = singleBrush,
                BrushKey = singleBrushKey
            });
        }

        ThemeBrushes = theme;
    }
}
