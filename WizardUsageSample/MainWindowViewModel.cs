using System;
using System.IO;
using System.Threading.Tasks;

namespace WizardUsageSample
{
    public class MainWindowViewModel : ViewModelBase
    {
        public Step CurrentStep
        {
            get => GetValue<Step>();
            set => SetValue(value);
        }

        public InstrumentData Instrument
        {
            get => GetValue<InstrumentData>();
            set => SetValue(value);
        }

        public MainWindowViewModel()
        {
            CurrentStep = Step.Detecting;
            Instrument = new InstrumentData();

            ManipulateInstrumentData();

            EventAggregator.Instance.StepChanged += (s, step) => CurrentStep = step;
        }

        private void ManipulateInstrumentData()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    var random = new Random();
                    Instrument.SerialNumber = random.Next(100_000, 999_999).ToString();
                    Instrument.Label = Path.GetRandomFileName();

                    await Task.Delay(1000);
                }
            });
        }
    }
}