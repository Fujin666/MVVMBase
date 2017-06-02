using System.ComponentModel;
using System.Runtime.CompilerServices;
using MVVMBase.Annotations;

namespace MVVMBase
{
    public class BindableBase : INotifyPropertyChanged, INotifyPropertyChanging, IChangeTracking
    {
        private bool _isChanged;

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        public bool SetProperty<T>(ref T target, T value, [CallerMemberName]string propertyName = null)
        {
            if (target != null && target.Equals(value)) return false;

            OnPropertyChanging(propertyName);

            target = value;

            _isChanged = true;

            OnPropertyChanged(propertyName);

            return true;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (propertyName == "IsChanged") return;

            PropertyChangedEventHandler handle = PropertyChanged;
            if (handle != null)
            {
                handle.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnPropertyChanging(string propertyName)
        {
            if (propertyName == "IsChanged") return;

            PropertyChangingEventHandler handle = PropertyChanging;
            if (handle != null)
            {
                handle.Invoke(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        public void AcceptChanges()
        {
            _isChanged = false;
        }

        public bool IsChanged { get { return _isChanged; } }
    }
}
