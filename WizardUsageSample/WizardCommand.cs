using System;
using System.Windows.Input;

namespace WizardUsageSample;

public class WizardCommand : ICommand
{
    private readonly Action action;
    private readonly Func<bool> canExecute;

    public event EventHandler CanExecuteChanged;
    
    public string Label { get; set; }

    public WizardCommand(Action action, Func<bool> canExecute = null)
    {
        this.action = action;
        this.canExecute = canExecute ?? (() => true);
    }

    public bool CanExecute(object parameter) => this.canExecute();

    public void Execute(object parameter)
    {
        this.action();
    }
}