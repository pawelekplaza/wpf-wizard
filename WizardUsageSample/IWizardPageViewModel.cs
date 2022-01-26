using System.Collections.Generic;

namespace WizardUsageSample;

public interface IWizardPageViewModel
{
    IEnumerable<WizardCommand> Commands { get; }
}