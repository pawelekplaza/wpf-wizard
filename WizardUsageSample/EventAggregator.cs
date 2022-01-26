using System;

namespace WizardUsageSample;

public class EventAggregator
{
    private static readonly Lazy<EventAggregator> instance = new(() => new EventAggregator());
    public static EventAggregator Instance => instance.Value;

    public event EventHandler<Step> StepChanged;

    public void RaiseStepChanged(Step step)
    {
        StepChanged?.Invoke(null, step);
    }
}