using System.Collections.Generic;

namespace WpfWizard;

public class SuccessViewModel : ViewModelBase, IWizardPageViewModel
{
    public IEnumerable<WizardCommand> Commands
    {
        get
        {
            yield return new WizardCommand(() => EventAggregator.Instance.RaiseStepChanged(Step.SelectSettings))
            {
                Label = "Back to select settings"
            };
            yield return new WizardCommand(() => { }, () => false)
            {
                Label = "Next"
            };
        }
    }
}