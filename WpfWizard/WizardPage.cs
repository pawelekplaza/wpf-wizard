using System.Windows;
using System.Windows.Controls;

namespace WpfWizard;

public class WizardPage : ContentControl
{
    public static readonly DependencyProperty EnumValueProperty = DependencyProperty.Register(
        "EnumValue", typeof(object), typeof(WizardPage), new PropertyMetadata(default(object)));

    public object EnumValue
    {
        get => GetValue(EnumValueProperty);
        set => SetValue(EnumValueProperty, value);
    }
}