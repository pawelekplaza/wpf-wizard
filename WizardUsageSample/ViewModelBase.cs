using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WizardUsageSample
{
    /// <summary>
    /// Base view model implementing <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private Dictionary<string, object> Storage { get; } = new();
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (Storage.ContainsKey(propertyName))
            {
                return (T)Storage[propertyName];
            }

            var value = default(T);
            Storage.Add(propertyName, value);
            return value;
        }

        protected bool SetValue<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (Storage.TryGetValue(propertyName, out var storedValue) && PropertyEquals((T)storedValue, value))
            {
                return false;
            }

            Storage[propertyName] = value;
            OnPropertyChanged(propertyName);

            return true;
        }
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static bool PropertyEquals<T>(T x, T y)
        {
            return EqualityComparer<T>.Default.Equals(x, y);
        }
    }
}