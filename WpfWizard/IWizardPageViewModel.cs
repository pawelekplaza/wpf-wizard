using System.Collections.Generic;

namespace WpfWizard;

public interface IWizardPageViewModel
{
    IEnumerable<WizardCommand> Commands { get; }
}