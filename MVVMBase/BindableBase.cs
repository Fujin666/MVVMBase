using System.ComponentModel;
using System.Runtime.CompilerServices;
using MVVMBase.Annotations;

namespace MVVMBase
{
    public class BindableBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        public bool SetProperty<T>(ref T target, T value, [CallerMemberName]string propertyName = null)
        {
            if (target != null && target.Equals(value)) return false;

            OnPropertyChanging(propertyName);

            target = value;

            OnPropertyChanged(propertyName);

            return true;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handle = PropertyChanged;
            if (handle != null)
            {
                handle.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnPropertyChanging(string propertyName)
        {
            PropertyChangingEventHandler handle = PropertyChanging;
            if (handle != null)
            {
                handle.Invoke(this, new PropertyChangingEventArgs(propertyName));
            }
        }
    }
}
