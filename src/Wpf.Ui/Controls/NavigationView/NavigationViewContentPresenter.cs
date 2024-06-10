// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

/* Based on Windows UI Library */

using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Wpf.Ui.Animations;

// ReSharper disable once CheckNamespace
namespace Wpf.Ui.Controls;

public class NavigationViewContentPresenter : Frame
{
    /// <summary>Identifies the <see cref="TransitionDuration"/> dependency property.</summary>
    public static readonly DependencyProperty TransitionDurationProperty = DependencyProperty.Register(
        nameof(TransitionDuration),
        typeof(int),
        typeof(NavigationViewContentPresenter),
        new FrameworkPropertyMetadata(200)
    );

    /// <summary>Identifies the <see cref="Transition"/> dependency property.</summary>
    public static readonly DependencyProperty TransitionProperty = DependencyProperty.Register(
        nameof(Transition),
        typeof(Transition),
        typeof(NavigationViewContentPresenter),
        new FrameworkPropertyMetadata(Transition.FadeInWithSlide)
    );

    /// <summary>Identifies the <see cref="IsDynamicScrollViewerEnabled"/> dependency property.</summary>
    public static readonly DependencyProperty IsDynamicScrollViewerEnabledProperty =
        DependencyProperty.Register(
            nameof(IsDynamicScrollViewerEnabled),
            typeof(bool),
            typeof(NavigationViewContentPresenter),
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsMeasure)
        );

    [Bindable(true)]
    [Category("Appearance")]
    public int TransitionDuration
    {
        get => (int)GetValue(TransitionDurationProperty);
        set => SetValue(TransitionDurationProperty, value);
    }

    /// <summary>
    /// Gets or sets type of <see cref="NavigationViewContentPresenter"/> transitions during navigation.
    /// </summary>
    public Transition Transition
    {
        get => (Transition)GetValue(TransitionProperty);
        set => SetValue(TransitionProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the dynamic scroll viewer is enabled.
    /// </summary>
    public bool IsDynamicScrollViewerEnabled
    {
        get => (bool)GetValue(IsDynamicScrollViewerEnabledProperty);
        protected set => SetValue(IsDynamicScrollViewerEnabledProperty, value);
    }

    static NavigationViewContentPresenter()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NavigationViewContentPresenter),
            new FrameworkPropertyMetadata(typeof(NavigationViewContentPresenter))
        );

        NavigationUIVisibilityProperty.OverrideMetadata(
            typeof(NavigationViewContentPresenter),
            new FrameworkPropertyMetadata(NavigationUIVisibility.Hidden)
        );

        SandboxExternalContentProperty.OverrideMetadata(
            typeof(NavigationViewContentPresenter),
            new FrameworkPropertyMetadata(true)
        );

        JournalOwnershipProperty.OverrideMetadata(
            typeof(NavigationViewContentPresenter),
            new FrameworkPropertyMetadata(JournalOwnership.UsesParentJournal)
        );

        ScrollViewer.CanContentScrollProperty.OverrideMetadata(
            typeof(Page),
            new FrameworkPropertyMetadata(true)
        );
    }

    public NavigationViewContentPresenter()
    {
        Navigating += static (sender, eventArgs) =>
        {
            if (eventArgs.Content is null)
            {
                return;
            }

            var self = (NavigationViewContentPresenter)sender;
            self.OnNavigating(eventArgs);
        };

        Navigated += static (sender, eventArgs) =>
        {
            var self = (NavigationViewContentPresenter)sender;

            if (eventArgs.Content is null)
            {
                return;
            }

            self.OnNavigated(eventArgs);
        };
    }

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);

        // REVIEW: I didn't understand something, but why is it necessary?
        Unloaded += static (sender, _) =>
        {
            if (sender is NavigationViewContentPresenter navigator)
            {
                NotifyContentAboutNavigatingFrom(navigator.Content);
            }
        };
    }

    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
        if (e.ChangedButton is MouseButton.XButton1 or MouseButton.XButton2)
        {
            e.Handled = true;
        }

        base.OnMouseDown(e);
    }

    protected virtual void OnNavigating(System.Windows.Navigation.NavigatingCancelEventArgs eventArgs)
    {
        NotifyContentAboutNavigatingTo(eventArgs.Content);

        if (eventArgs.Navigator is not NavigationViewContentPresenter navigator)
        {
            return;
        }

        NotifyContentAboutNavigatingFrom(navigator.Content);
    }

    protected virtual void OnNavigated(NavigationEventArgs eventArgs)
    {
        ApplyTransitionEffectToNavigatedPage(eventArgs.Content);

        if (eventArgs.Content is not DependencyObject dependencyObject)
        {
            return;
        }

        SetCurrentValue(
            IsDynamicScrollViewerEnabledProperty,
            ScrollViewer.GetCanContentScroll(dependencyObject)
        );
    }

    private void ApplyTransitionEffectToNavigatedPage(object content)
    {
        if (TransitionDuration < 1)
        {
            return;
        }

        _ = TransitionAnimationProvider.ApplyTransition(content, Transition, TransitionDuration);
    }

    private static void NotifyContentAboutNavigatingTo(object content)
    {
        switch (content)
        {
            case INavigationAware navigationAwareNavigationContent:
                navigationAwareNavigationContent.OnNavigatedTo();
                break;
            case INavigableView<object>
            {
                ViewModel: INavigationAware navigationAwareNavigableViewViewModel
            }:
                navigationAwareNavigableViewViewModel.OnNavigatedTo();
                break;
            case FrameworkElement { DataContext: INavigationAware navigationAwareCurrentContent }:
                navigationAwareCurrentContent.OnNavigatedTo();
                break;
        }
    }

    private static void NotifyContentAboutNavigatingFrom(object content)
    {
        switch (content)
        {
            case INavigationAware navigationAwareNavigationContent:
                navigationAwareNavigationContent.OnNavigatedFrom();
                break;
            case INavigableView<object>
            {
                ViewModel: INavigationAware navigationAwareNavigableViewViewModel
            }:
                navigationAwareNavigableViewViewModel.OnNavigatedFrom();
                break;
            case FrameworkElement { DataContext: INavigationAware navigationAwareCurrentContent }:
                navigationAwareCurrentContent.OnNavigatedFrom();
                break;
        }
    }
}
