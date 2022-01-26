using System.Collections.Generic;

namespace WpfWizard;

public class InstrumentData : ViewModelBase, IWizardPageViewModel
{
    public string SerialNumber
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public string Label
    {
        get => GetValue<string>();
        set => SetValue(value);
    }

    public IEnumerable<WizardCommand> Commands
    {
        get
        {
            yield return new WizardCommand(() => EventAggregator.Instance.RaiseStepChanged(Step.Detecting))
            {
                Label = "Back to detecting"
            };
            yield return new WizardCommand(() => EventAggregator.Instance.RaiseStepChanged(Step.Success))
            {
                Label = "Next"
            };
        }
    }
}