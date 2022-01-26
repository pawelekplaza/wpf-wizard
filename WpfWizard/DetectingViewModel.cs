using System.Collections.Generic;

namespace WpfWizard;

public class DetectingViewModel : ViewModelBase, IWizardPageViewModel
{
    public IEnumerable<WizardCommand> Commands
    {
        get
        {
            yield return new WizardCommand(() => EventAggregator.Instance.RaiseStepChanged(Step.Unknown))
            {
                Label = "Back"
            };
            yield return new WizardCommand(() => EventAggregator.Instance.RaiseStepChanged(Step.SelectSettings))
            {
                Label = "Next"
            };
        }
    }
}