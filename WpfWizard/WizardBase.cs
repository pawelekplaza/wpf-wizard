﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfWizard;

[TemplatePart(Name = WizardPageName, Type = typeof(WizardPageHost))]
public abstract class WizardBase<T> : ContentControl
{
    private const string WizardPageName = "PART_WizardPageHost"; 
    
    private WizardPageHost wizardPageHostContent;
        
    public static readonly DependencyProperty EnumTypeProperty = DependencyProperty.Register(
        "EnumType", typeof(Type), typeof(WizardBase<T>), new PropertyMetadata(default(Type)));
    
    public static readonly DependencyProperty EnumSourceProperty = DependencyProperty.Register(
        "EnumSource", typeof(Enum), typeof(WizardBase<T>), new PropertyMetadata(default(Enum), EnumSourceChanged));
    
    public static readonly DependencyProperty PagesProperty = DependencyProperty.Register(
        "Pages", typeof(ObservableCollection<WizardPage>), typeof(WizardBase<T>), new PropertyMetadata(default(ObservableCollection<WizardPage>)));

    public static readonly DependencyProperty CurrentPageProperty = DependencyProperty.Register(
        "CurrentPage", typeof(ContentControl), typeof(WizardBase<T>), new PropertyMetadata(default(ContentControl), CurrentPageChanged));

    public static readonly DependencyProperty CurrentPageViewModelProperty = DependencyProperty.Register(
        "CurrentPageViewModel", typeof(T), typeof(WizardBase<T>), new PropertyMetadata(default(T)));

    public Type EnumType
    {
        get => (Type) GetValue(EnumTypeProperty);
        set => SetValue(EnumTypeProperty, value);
    }
    
    public Enum EnumSource
    {
        get => (Enum) GetValue(EnumSourceProperty);
        set => SetValue(EnumSourceProperty, value);
    }
    
    public ObservableCollection<WizardPage> Pages
    {
        get => (ObservableCollection<WizardPage>) GetValue(PagesProperty);
        set => SetValue(PagesProperty, value);
    }
    
    public ContentControl CurrentPage
    {
        get => (ContentControl) GetValue(CurrentPageProperty);
        private set => SetValue(CurrentPageProperty, value);
    }
    
    public T CurrentPageViewModel
    {
        get => (T) GetValue(CurrentPageViewModelProperty);
        private set => SetValue(CurrentPageViewModelProperty, value);
    }

    protected WizardBase()
    {
        Pages = new ObservableCollection<WizardPage>();
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (!EnumType.IsEnum)
        {
            throw new ArgumentException("Provided type for EnumType property is not an enum.");
        }

        this.wizardPageHostContent = GetTemplateChild(WizardPageName) as WizardPageHost 
            ?? throw new InvalidOperationException($"Template does not have a wizard page host with '{WizardPageName}' name.");
        
        SetHostedPage(EnumSource);
    }
    
    private static void EnumSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not WizardBase<T> wizard)
        {
            return;
        }

        wizard.SetHostedPage(e.NewValue);
    }
    
    private static void CurrentPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not WizardBase<T> wizard)
        {
            return;
        }

        if (wizard.CurrentPage.DataContext is T wizardPageViewModel)
        {
            wizard.SetValue(CurrentPageViewModelProperty, wizardPageViewModel);
            return;
        }

        if (wizard.CurrentPage.Content is T contentWizardPageViewModel)
        {
            wizard.SetValue(CurrentPageViewModelProperty, contentWizardPageViewModel);
        }
    }

    private void SetHostedPage(object enumValue)
    {
        var pageToBeHosted = Pages.FirstOrDefault(x => x.EnumValue?.Equals(enumValue) == true);
        if (pageToBeHosted == null)
        {
            return;
        }

        this.wizardPageHostContent.Content = pageToBeHosted;
        SetValue(CurrentPageProperty, pageToBeHosted);
    }
}