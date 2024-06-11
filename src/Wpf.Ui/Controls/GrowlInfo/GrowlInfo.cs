// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.
using System.Windows.Threading;
using Wpf.Ui.Input;

namespace Wpf.Ui.Controls;

public class GrowlInfo : System.Windows.Controls.ContentControl
{
    /// <summary>Identifies the <see cref="IsOpen"/> dependency property.</summary>
    public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(GrowlInfo), new PropertyMetadata(true));

    /// <summary>Identifies the <see cref="Title"/> dependency property.</summary>
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(GrowlInfo), new PropertyMetadata(string.Empty));

    /// <summary>Identifies the <see cref="ConfirmButtonText"/> dependency property.</summary>
    public static readonly DependencyProperty ConfirmButtonTextProperty = DependencyProperty.Register(nameof(ConfirmButtonText), typeof(string), typeof(GrowlInfo), new PropertyMetadata("Confirm"));

    /// <summary>Identifies the <see cref="CancelButtonText"/> dependency property.</summary>
    public static readonly DependencyProperty CancelButtonTextProperty = DependencyProperty.Register(nameof(CancelButtonText), typeof(string), typeof(GrowlInfo), new PropertyMetadata("Cancel"));

    /// <summary>Identifies the <see cref="IsShowTitle"/> dependency property.</summary>
    public static readonly DependencyProperty IsShowTitleProperty = DependencyProperty.Register(nameof(IsShowTitle), typeof(bool), typeof(GrowlInfo), new PropertyMetadata(true));

    /// <summary>Identifies the <see cref="Message"/> dependency property.</summary>
    public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(nameof(Message), typeof(string), typeof(GrowlInfo), new PropertyMetadata(string.Empty));

    /// <summary>Identifies the <see cref="Severity"/> dependency property.</summary>
    public static readonly DependencyProperty SeverityProperty = DependencyProperty.Register(nameof(Severity), typeof(GrowlInfoSeverity), typeof(GrowlInfo), new PropertyMetadata(GrowlInfoSeverity.Informational));

    /// <summary>Identifies the <see cref="NowDateTime"/> dependency property.</summary>
    public static readonly DependencyProperty NowDateTimeProperty = DependencyProperty.Register(nameof(NowDateTime), typeof(string), typeof(GrowlInfo), new PropertyMetadata(DateTime.Now.ToString()));

    /// <summary>Identifies the <see cref="TimeOut"/> dependency property.</summary>
    public static readonly DependencyProperty TimeOutProperty = DependencyProperty.Register(nameof(TimeOut), typeof(int), typeof(GrowlInfo), new PropertyMetadata(3000));

    /// <summary>Identifies the <see cref="IsShowNowDateTime"/> dependency property.</summary>
    public static readonly DependencyProperty IsShowNowDateTimeProperty = DependencyProperty.Register(nameof(IsShowNowDateTime), typeof(bool), typeof(GrowlInfo), new PropertyMetadata(true));

    /// <summary>Identifies the <see cref="TemplateCloseButtonCommand"/> dependency property.</summary>
    public static readonly DependencyProperty TemplateCloseButtonCommandProperty = DependencyProperty.Register(
    nameof(TemplateCloseButtonCommand),
    typeof(IRelayCommand),
    typeof(GrowlInfo),
    new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="TemplateConfirmButtonCommand"/> dependency property.</summary>
    public static readonly DependencyProperty TemplateConfirmButtonCommandProperty = DependencyProperty.Register(
    nameof(TemplateConfirmButtonCommand),
    typeof(IRelayCommand),
    typeof(GrowlInfo),
    new PropertyMetadata(null));

    /// <summary>Identifies the <see cref="TemplateCancelButtonCommand"/> dependency property.</summary>
    public static readonly DependencyProperty TemplateCancelButtonCommandProperty = DependencyProperty.Register(
    nameof(TemplateCancelButtonCommand),
    typeof(IRelayCommand),
    typeof(GrowlInfo),
    new PropertyMetadata(null));

    /// <summary>
    /// Gets the <see cref="RelayCommand{T}"/> triggered after clicking
    /// the close button.
    /// </summary>
    public IRelayCommand TemplateCloseButtonCommand => (IRelayCommand)GetValue(TemplateCloseButtonCommandProperty);

    /// <summary>
    /// Gets the <see cref="RelayCommand{T}"/> triggered after clicking
    /// the confirm button.
    /// </summary>
    public IRelayCommand TemplateConfirmButtonCommand => (IRelayCommand)GetValue(TemplateConfirmButtonCommandProperty);

    /// <summary>
    /// Gets the <see cref="RelayCommand{T}"/> triggered after clicking
    /// the cance button.
    /// </summary>
    public IRelayCommand TemplateCancelButtonCommand => (IRelayCommand)GetValue(TemplateCancelButtonCommandProperty);

    /// <summary>
    /// GrowlInfo Closed delegation
    /// </summary>
    public delegate void GrowlInfoCloseedEventHandler(object sender);

    /// <summary>
    /// GrowlInfo Confirm delegation triggered after clicking
    /// </summary>
    public delegate void ConfirmButtonClickEventHandler(object sender);

    /// <summary>
    /// GrowlInfo Cance delegation triggered after clicking
    /// </summary>
    public delegate void CancelButtonClickEventHandler(object sender);

    /// <summary>
    /// This event occurred after GrowlInfo was closed
    /// </summary>
    [Category("Behavior")]
    public event GrowlInfoCloseedEventHandler? GrowlInfoCloseed;

    /// <summary>
    /// This event occurs after clicking the confirm button
    /// triggered after clicking
    /// </summary>
    [Category("Behavior")]
    public event ConfirmButtonClickEventHandler? ConfirmButtonClick;

    /// <summary>
    /// This event occurs after clicking the cancel button
    /// triggered after clicking
    /// </summary>
    [Category("Behavior")]
    public event CancelButtonClickEventHandler? CancelButtonClick;

    /// <summary>
    /// Gets or sets a value indicating whether the user can open the <see cref="GrowlInfo" />. Defaults to <c>true</c>.
    /// </summary>
    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    /// <summary>
    /// Gets or sets the title of the <see cref="GrowlInfo" />.
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>
    /// Gets or sets the message of the <see cref="GrowlInfo" />.
    /// </summary>
    public string Message
    {
        get => (string)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    /// <summary>
    /// Gets or sets the type of the <see cref="GrowlInfo" /> to apply
    /// consistent status color, icon, and assistive technology settings
    /// dependent on the criticality of the notification.
    /// </summary>
    public GrowlInfoSeverity Severity
    {
        get => (GrowlInfoSeverity)GetValue(SeverityProperty);
        set => SetValue(SeverityProperty, value);
    }

    /// <summary>
    /// Gets or sets the nowtime of the <see cref="GrowlInfo" />.
    /// </summary>
    public string NowDateTime
    {
        get => (string)GetValue(NowDateTimeProperty);
        set => SetValue(NowDateTimeProperty, value);
    }

    /// <summary>
    /// Gets or sets the timeout of the <see cref="GrowlInfo" />.
    /// </summary>
    public int TimeOut
    {
        get => (int)GetValue(TimeOutProperty);
        set => SetValue(TimeOutProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user can show message time the <see cref="GrowlInfo" />. Defaults to <c>true</c>.
    /// </summary>
    public bool IsShowNowDateTime
    {
        get => (bool)GetValue(IsShowNowDateTimeProperty);
        set => SetValue(IsShowNowDateTimeProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user can show message time the <see cref="GrowlInfo" />. Defaults to <c>true</c>.
    /// </summary>
    public bool IsShowTitle
    {
        get => (bool)GetValue(IsShowTitleProperty);
        set => SetValue(IsShowTitleProperty, value);
    }

    /// <summary>
    /// Gets or sets the text to be displayed on the confirm button of the <see cref="GrowlInfo" />.
    /// </summary>
    public string ConfirmButtonText
    {
        get => (string)GetValue(ConfirmButtonTextProperty);
        set => SetValue(ConfirmButtonTextProperty, value);
    }

    /// <summary>
    /// Gets or sets the text to display on the cancel button of the <see cref="GrowlInfo" />.
    /// </summary>
    public string CancelButtonText
    {
        get => (string)GetValue(CancelButtonTextProperty);
        set => SetValue(CancelButtonTextProperty, value);
    }

    private DispatcherTimer? dispatcherTimer;

    public GrowlInfo()
    {
        SetValue(
            TemplateCloseButtonCommandProperty,
            new RelayCommand<object>(_ =>
            {
                SetCurrentValue(IsOpenProperty, false);

                _ = Task.Delay(300).ContinueWith(task =>
                {
                    _ = this.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        var parent = (System.Windows.Controls.StackPanel)this.Parent;
                        parent.Children.Remove(this);
                    })).Wait();
                });

                GrowlInfoCloseed?.Invoke(this);
            }));

        SetValue(
            TemplateConfirmButtonCommandProperty,
            new RelayCommand<object>(_ =>
            {
                SetCurrentValue(IsOpenProperty, false);

                _ = Task.Delay(300).ContinueWith(task =>
                {
                    _ = this.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        var parent = (System.Windows.Controls.StackPanel)this.Parent;
                        parent.Children.Remove(this);
                    })).Wait();
                });

                ConfirmButtonClick?.Invoke(this);
            }));

        SetValue(
            TemplateCancelButtonCommandProperty,
            new RelayCommand<object>(_ =>
            {
                SetCurrentValue(IsOpenProperty, false);

                _ = Task.Delay(300).ContinueWith(task =>
                {
                    _ = this.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        var parent = (System.Windows.Controls.StackPanel)this.Parent;
                        parent.Children.Remove(this);
                    })).Wait();
                });

                CancelButtonClick?.Invoke(this);
            }));
    }

    private void GrowlInfo_Loaded(object sender, RoutedEventArgs e)
    {
        if (Severity != GrowlInfoSeverity.Inquiry)
        {
            dispatcherTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(TimeOut)
            };

            dispatcherTimer.Start();
        }
    }
}
