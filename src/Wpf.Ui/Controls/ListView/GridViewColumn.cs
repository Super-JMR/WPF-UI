// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Reflection;

namespace Wpf.Ui.Controls;

public class GridViewColumn : System.Windows.Controls.GridViewColumn
{
    // use reflection to get the `DesiredWidth` internal property. cache the `PropertyInfo` for performance
    private static readonly PropertyInfo _desiredWidthProperty = typeof(System.Windows.Controls.GridViewColumn).GetProperty("DesiredWidth", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException("The `DesiredWidth` property was not found.");

    public double DesiredWidth => (double)(_desiredWidthProperty.GetValue(this) ?? throw new InvalidOperationException("The `DesiredWidth` property was not found."));

    // use reflection to get the `ActualIndex` internal property. cache the `PropertyInfo` for performance
    private static readonly PropertyInfo _actualIndexProperty = typeof(System.Windows.Controls.GridViewColumn).GetProperty("ActualIndex", BindingFlags.Instance | BindingFlags.NonPublic) ?? throw new InvalidOperationException("The `ActualIndex` property was not found.");

    public int ActualIndex => (int)(_actualIndexProperty.GetValue(this) ?? throw new InvalidOperationException("The `ActualIndex` property was not found."));

    public double MinWidth
    {
        get => (double)GetValue(MinWidthProperty);
        set => SetValue(MinWidthProperty, value);
    }

    /// <summary>Identifies the <see cref="MinWidth"/> dependency property.</summary>
    public static readonly DependencyProperty MinWidthProperty = DependencyProperty.Register(nameof(MinWidth), typeof(double), typeof(GridViewColumn), new FrameworkPropertyMetadata(0.0, OnMinWidthChanged));

    private static void OnMinWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not GridViewColumn self)
        {
            return;
        }

        self.OnMinWidthChanged(e);
    }

    protected virtual void OnMinWidthChanged(DependencyPropertyChangedEventArgs e)
    {
    }

    public double MaxWidth
    {
        get => (double)GetValue(MaxWidthProperty);
        set => SetValue(MaxWidthProperty, value);
    }

    /// <summary>Identifies the <see cref="MaxWidth"/> dependency property.</summary>
    public static readonly DependencyProperty MaxWidthProperty = DependencyProperty.Register(nameof(MaxWidth), typeof(double), typeof(GridViewColumn), new FrameworkPropertyMetadata(Double.PositiveInfinity, OnMaxWidthChanged));

    private static void OnMaxWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not GridViewColumn self)
        {
            return;
        }

        self.OnMaxWidthChanged(e);
    }

    protected virtual void OnMaxWidthChanged(DependencyPropertyChangedEventArgs e)
    {
    }
}