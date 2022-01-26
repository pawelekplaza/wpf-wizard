using System.Collections.Generic;

namespace WizardUsageSample;

public class UnknownViewModel : ViewModelBase, IWizardPageViewModel
{
    public IEnumerable<WizardCommand> Commands
    {
        get
        {
            yield return new WizardCommand(() => { }, () => false)
            {
                Label = "Back"
            };
            yield return new WizardCommand(() => EventAggregator.Instance.RaiseStepChanged(Step.Detecting))
            {
                Label = "Continue to detecting"
            };
        }
    }
}